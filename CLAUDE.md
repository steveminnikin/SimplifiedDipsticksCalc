# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is an ASP.NET MVC web application written in VB.NET that calculates dipstick calibration tables for various tank geometries. The application generates volume-to-height conversion tables for tanks containing liquids, supporting different tank shapes and measurement systems.

## Build & Run Commands

### Build the project
```bash
msbuild SimplifiedDipsticksCalc.sln /p:Configuration=Debug
# or
msbuild SimplifiedDipsticksCalc.sln /p:Configuration=Release
```

### Run the application
The application runs on IIS Express (configured at http://localhost:51564/). Open the solution in Visual Studio and press F5, or use:
```bash
# Start IIS Express (requires Visual Studio/IIS Express installed)
"C:\Program Files\IIS Express\iisexpress.exe" /path:"C:\Users\steve\OneDrive\Code\SimplifiedDipsticksCalc\SimplifiedDipsticksCalc" /port:51564
```

### Restore NuGet packages
```bash
nuget restore SimplifiedDipsticksCalc.sln
```

## Architecture

### Core Components

**Tank Hierarchy**: All tank types inherit from the base `Tank` class (Models/Tank.vb):
- `Tank` - Base class with common properties (FullVol, Dimensions, Increments, IncrementList, RegDip)
- `Rectangular` - Rectangular tanks with optional tilt/slope
- `VertCyl` - Vertical cylindrical tanks with optional dished ends
- `HorizFlatEnds` - Horizontal cylindrical tanks with flat ends
- `HorizDishEnds` - Horizontal cylindrical tanks with dished ends
- `Elliptical` - Elliptical cross-section tanks

### Service Layer Pattern

Each tank type has a corresponding service class in the `Services` folder that:
1. Inherits from `TankService` base class
2. Implements dimension conversion (converting user inputs to centimeters)
3. Calculates full volume
4. Generates increment lists (dipstick calibration points)

The service classes use a `IInitialConversionValues` structure to handle unit conversions between:
- Litres & Millimetres
- Gallons & Inches
- Gallons & Millimetres
- US Gallons & Millimetres
- US Gallons & Inches
- Cubic Metres & Millimetres

### Controller Pattern

Controllers (in `Controllers` folder) follow this pattern:
1. Accept POST requests with tank dimensions and parameters
2. Use `TankService.GetinitialConversionValues()` to get conversion factors
3. Call shape-specific service methods to convert dimensions
4. Call `CalculateIncrements()` to generate the dipstick table
5. Return the `Calculate.vbhtml` view with results

### View Structure

The application uses a multi-page architecture with separate views for each tank type:
- `Views/Home/Index.vbhtml` - Landing page with tank type selection cards
- `Views/{TankType}/Index.vbhtml` - Input form for each tank type (Rectangular, VertCyl, HorizFlatEnds, HorizDishEnds, Elliptical)
- `Views/{TankType}/Calculate.vbhtml` - Results display for each tank type

Shared partials include:
- `_ClientPartial.vbhtml` - Client information form
- `_DimensionsPartial.vbhtml` - Unit selection and common parameters
- `_IncrementsView.vbhtml` - Display of calculated increment table

**Important**: Each tank type MUST have its own Index.vbhtml file in the corresponding Views folder, and these files MUST be included in the .vbproj file as `<Content>` items for deployment to work properly.

### JavaScript Functionality

`Scripts/custom.js` handles:
- Persisting form data in sessionStorage across page navigations
- Dynamically setting form action based on active tab
- Client information display toggle
- Editable output mode for customization
- Note addition to charts

### Key Business Logic

**Increment Calculation**: Two modes controlled by `RegDip` property:
- `RegDip = True`: Fixed height increments, calculates volume at each height
- `RegDip = False`: Fixed volume increments, calculates height at each volume

**Tilt/Slope Handling**: Rectangular tanks support tilt calculations for sloped tank bottoms, using complex geometry in `RectangularService.TiltCalc()`

**Engrave Code Export**: `TankService.DownloadEngraveCode()` generates CSV files for CNC engraving machines

**Conversion Rounding**: `Tank.FinalConversionRounding()` applies different rounding rules based on unit system and RegDip mode

## VB.NET Specifics

- `Option Strict` is OFF - be aware of implicit conversions
- Uses VB.NET `IIf()` function extensively (inline if-else)
- Properties use VB.NET auto-property syntax
- `Nullable(Of T)` syntax for nullable types
- Imports statements equivalent to C# using directives

## Deployment

### Azure App Service

The application is deployed to Azure App Service at `dipstickscalc.azurewebsites.net`.

**Critical Deployment Requirements:**

1. **All view files must be in .vbproj**: Any `.vbhtml` files that exist in the filesystem but are NOT listed as `<Content>` items in `SimplifiedDipsticksCalc.vbproj` will NOT be deployed to Azure. This was the root cause of Bug #4 where all tank calculator Index views were missing from production.

2. **Roslyn compiler files**: The project includes a custom MSBuild target (`CopyRoslynFiles`) to ensure Roslyn compiler files are deployed to `bin/roslyn`. This is required for runtime view compilation on Azure.

3. **Error visibility**: Set `<customErrors mode="RemoteOnly" />` in Web.config for production (shows detailed errors locally but generic errors on Azure).

**If the app works locally but fails on Azure with "view not found" errors:**
- Check that all `.vbhtml` files are listed in the `.vbproj` file
- Verify the files have `<Content Include="Views/...">` entries
- Rebuild and republish the application

## Important Notes

- The application generates XML documentation (SimplifiedDipsticksCalc.xml) during build
- ApplicationInsights is configured for telemetry
- Target framework is .NET Framework 4.5.2 (Web.config shows 4.8 for compilation)
- Uses classic ASP.NET MVC 5 (not .NET Core)
- See bugs.md for comprehensive list of known issues and fixes
