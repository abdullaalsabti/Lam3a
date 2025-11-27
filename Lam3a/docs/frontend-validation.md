# Frontend Validation Guide

This document consolidates all validations that currently run on the API for create/update flows. Mirror these checks on the client so requests fail fast and users get immediate feedback.

## Auth Endpoints

### `POST /api/auth/register`
- **DTO (FluentValidation)**
  - `Email` required; must be a valid email format.
  - `Password` required; minimum length 6 characters.
  - `Role` must be either `Client` or `Provider`; when sending integers use `0` for `Client`, `1` for `Provider` (see enum table below).
- **Controller logic**
  - Rejects if the email is already registered (`Users.Email` unique check).

### `POST /api/auth/verify-phone`
- **DTO (FluentValidation)**
  - `UserId` required (non-empty `Guid`).
  - `FirebaseIdToken` required.
  - `Phone` required; must match `+9627XXXXXXXX` (Jordanian mobile in E.164 format).
- **Controller logic / Firebase**
  - Decodes `FirebaseIdToken` and ensures a `phone_number` claim exists.
  - Phone from Firebase must match the `Phone` sent by the client.
  - User must exist; otherwise request is rejected.

### `POST /api/auth/login`
- **DTO (FluentValidation)**
  - `Email` required; must be a valid email.
  - `Password` required; minimum length 6.
- **Controller logic**
  - User credentials must match an existing account; password is verified against PBKDF2 hash with pepper.

### `POST /api/auth/refreshToken`
- **DTO**
  - `RefreshToken` string required (no explicit FluentValidation; enforce non-empty client-side).
- **Controller logic**
  - Token must exist, not revoked, not used, and not expired.
  - Associated user must still exist; otherwise the refresh is rejected.

## Client Profile Endpoints

### `PUT /api/client/clientprofile/editProfile`
- **DTO (FluentValidation)**
  - `FirstName`, `LastName` required; max length 50.
  - `Gender` must be a defined enum value (`Male`, `Female`, ...).
  - `DateOfBirth` required; must be in the past but not older than 120 years.
  - `Address` object validated via `AddressDtoValidator` (see below).
- **Nested Address validation**
  - `Street` required; max length 100.
  - `BuildingNumber` required; max length 5.
  - `Landmark` optional; max length 100.
  - `Coordinates.Latitude` between -90 and 90.
  - `Coordinates.Longitude` between -180 and 180.
- **Controller assumptions**
  - Endpoint is only accessible to authenticated clients (see `ClientAuthorizeAttribute`).

### `PUT /api/client/clientprofile/editAddress`
- **DTO (FluentValidation)**
  - Same `AddressDto` rules listed above apply.

## Vehicle Endpoints

All vehicle endpoints require the caller to be an authenticated client via `ClientAuthorizeAttribute`.

### `POST /api/client/vehicle`
- **DTO (FluentValidation)**
  - `PlateNumber` required; length between 2 and 7 characters.
  - `BrandId` required (non-zero integer).
  - `ModelId` required (non-zero integer).
  - `Color` / `CarType` are enums; backend accepts any defined enum value (validate selection client-side).
- **Action filter (`BrandModelFilter`)**
  - Confirms the referenced brand exists.
  - Confirms the referenced model exists and belongs to that brand.
- **Controller logic**
  - Rejects if a vehicle with the same `PlateNumber` already exists for any user.

### `PUT /api/client/vehicle/{plateNumber}`
- **DTO (FluentValidation & filter)**
  - Same rules as vehicle creation (`VehicleDTOValidator` + `BrandModelFilter`).
- **Controller logic**
  - Target vehicle must exist; otherwise returns `404`.

## Cross-Cutting Rules
- `ClientAuthorizeAttribute` ensures authenticated JWT, role `Client`, and loads the client record (with address) before controller logic runs.
- Entity models mark several properties as required (`User.Email`, `User.PasswordHash`, `Address.Street`, etc.). Treat these as mandatory on the frontend even if not re-validated by FluentValidation.
- Enum-backed fields (e.g., `Role`, `Gender`, `CarType`, `VehicleColor`) should only allow known values; the backend will reject invalid enum names during model binding.

> **Note:** Some inputs (e.g., refresh tokens) lack explicit FluentValidation rules; enforce basic presence checks on the frontend to keep user feedback immediate.

## Enum Numeric Mapping

When the frontend sends enum values as integers, use the exact ordering below (0-based, as defined in the backend `Enums/` folder).

| Enum | Value | Name |
| --- | --- | --- |
| `Role` | 0 | Client |
|  | 1 | Provider |
|  | 2 | Admin |
| `Gender` | 0 | Male |
|  | 1 | Female |
| `CarType` | 0 | Sadan |
|  | 1 | Suv |
|  | 2 | Bike |
| `VehicleColor` | 0 | Black |
|  | 1 | White |
|  | 2 | Silver |
|  | 3 | Blue |
|  | 4 | Red |
|  | 5 | Gray |
|  | 6 | Green |
| `PaymentMethod` | 0 | Cash |
|  | 1 | Cliq |
| `ServiceStatus` | 0 | OrderPlaced |
|  | 1 | ProviderOnTheWay |
|  | 2 | ProviderArrived |
|  | 3 | WashingStarted |
|  | 4 | Paying |
|  | 5 | Completed |
|  | 6 | Cancelled |
| `UserAccountStatus` | 0 | Unverified |
|  | 1 | Verified |
|  | 2 | LockedOut |
|  | 3 | Invalid |

> If you prefer to send enum names (`"Client"`, `"Male"`, ...), enable string enums via `JsonStringEnumConverter`; otherwise keep sending the numeric values above.

