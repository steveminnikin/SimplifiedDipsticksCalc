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

The main interface (`Views/Home/Index.vbhtml`) uses Bootstrap tabs to switch between tank types. Each tank type has:
- A partial view for input form (e.g., `_RectangularPartial.vbhtml`)
- A Calculate view to display results (e.g., `Rectangular/Calculate.vbhtml`)

Shared partials include:
- `_ClientPartial.vbhtml` - Client information form
- `_DimensionsPartial.vbhtml` - Unit selection and common parameters
- `_IncrementsView.vbhtml` - Display of calculated increment table

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

## Important Notes

- The application generates XML documentation (SimplifiedDipsticksCalc.xml) during build
- ApplicationInsights is configured for telemetry
- Target framework is .NET Framework 4.5.2
- Uses classic ASP.NET MVC 5 (not .NET Core)
