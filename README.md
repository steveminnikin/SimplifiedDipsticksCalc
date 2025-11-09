# Simplified Dipsticks Calculator

A web-based application for calculating dipstick calibration tables for various tank geometries. This ASP.NET MVC application generates accurate volume-to-height conversion tables for tanks containing liquids, supporting multiple tank shapes and measurement systems.

## Features

### Supported Tank Types

- **Rectangular Tanks** - With optional tilt/slope calculations
- **Vertical Cylindrical Tanks** - With optional dished ends
- **Horizontal Cylindrical Tanks** - Both flat and dished ends
- **Elliptical Tanks** - Elliptical cross-section tanks

### Measurement Systems

- Litres & Millimetres
- Gallons & Inches
- Gallons & Millimetres
- US Gallons & Millimetres
- US Gallons & Inches
- Cubic Metres & Millimetres

### Calculation Modes

1. **Regular Dipstick (RegDip)** - Fixed height increments, calculates volume at each height
2. **Workshop Format** - Fixed volume increments, calculates height at each volume

### Output Features

- Interactive web-based calculator
- Editable increment tables
- CSV export for CNC engraving machines
- Client information tracking
- Session-based form data persistence

## Technology Stack

- **Framework:** ASP.NET MVC 5
- **Language:** VB.NET
- **Target:** .NET Framework 4.5.2
- **UI:** Bootstrap, jQuery
- **Build System:** MSBuild
- **Package Manager:** NuGet

## Getting Started

### Prerequisites

- Visual Studio 2015 or later (with VB.NET support)
- .NET Framework 4.5.2 or later
- IIS Express (included with Visual Studio)
- NuGet Package Manager

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/steveminnikin/SimplifiedDipsticksCalc.git
   cd SimplifiedDipsticksCalc
   ```

2. Restore NuGet packages:
   ```bash
   nuget restore SimplifiedDipsticksCalc.sln
   ```

3. Build the solution:
   ```bash
   msbuild SimplifiedDipsticksCalc.sln /p:Configuration=Release
   ```

### Running the Application

#### Using Visual Studio
1. Open `SimplifiedDipsticksCalc.sln` in Visual Studio
2. Press `F5` to build and run
3. The application will launch at `http://localhost:51564/`

#### Using IIS Express (Command Line)
```bash
"C:\Program Files\IIS Express\iisexpress.exe" /path:"C:\path\to\SimplifiedDipsticksCalc\SimplifiedDipsticksCalc" /port:51564
```

## Usage

1. **Select Tank Type** - Choose from the available tank geometry tabs
2. **Enter Dimensions** - Input tank dimensions in your preferred units
3. **Set Parameters** - Configure increments and calculation mode
4. **Calculate** - Generate the dipstick calibration table
5. **Export** - Download the results as CSV for engraving

## Project Structure

```
SimplifiedDipsticksCalc/
├── Controllers/           # MVC Controllers for each tank type
├── Models/               # Tank model classes
├── Services/             # Business logic and calculation services
│   ├── TankService.vb           # Base service with common functionality
│   ├── RectangularService.vb    # Rectangular tank calculations
│   ├── VertCylService.vb        # Vertical cylinder calculations
│   ├── HorizFlatEndsService.vb  # Horizontal flat-end calculations
│   ├── HorizDishEndsService.vb  # Horizontal dished-end calculations
│   └── EllipticalService.vb     # Elliptical tank calculations
├── Views/                # Razor views (VB.NET)
│   ├── Home/                    # Main interface
│   ├── Rectangular/             # Rectangular tank views
│   ├── VertCyl/                 # Vertical cylinder views
│   ├── HorizFlatEnds/           # Horizontal flat-end views
│   ├── HorizDishEnds/           # Horizontal dished-end views
│   ├── Elliptical/              # Elliptical tank views
│   └── Shared/                  # Shared partial views
├── Scripts/              # JavaScript files
│   └── custom.js                # Form persistence and UI logic
└── Content/              # CSS and static assets
```

## Architecture

### Service Layer Pattern

Each tank type follows a consistent pattern:

1. **Model** - Defines tank properties and dimensions
2. **Service** - Handles:
   - Unit conversion
   - Volume calculation
   - Increment generation (dipstick calibration points)
3. **Controller** - Orchestrates data flow between views and services
4. **View** - User interface for input and results display

### Calculation Flow

```
User Input → Controller → Service.ConvertDimensions()
                      ↓
                  Service.CalculateFullVolume()
                      ↓
                  Service.CalculateIncrements()
                      ↓
                  View (Display Results)
```

## Key Algorithms

### Convergence Algorithm
For horizontal tanks (flat and dished ends, elliptical), the application uses iterative convergence to find accurate height-to-volume relationships:

```vb
Do
    T9 = TN
    xFactors(tank)
Loop Until Abs(TN - T9) <= 0.00001
```

### Tilt Calculations
Rectangular tanks with tilted bottoms use complex geometry to calculate volumes in the sloped section before transitioning to the regular rectangular section.

## Recent Bug Fixes

The codebase has undergone a comprehensive bug review and fix. See [bugs.md](bugs.md) for details.

**Fixed Issues:**
- ✅ 3 Critical bugs (loop conditions, initialization)
- ✅ 6 High severity bugs (division by zero, null references)
- 📋 13 Moderate/Low severity issues documented

## Configuration

### Application Settings

Key settings in `Web.config`:
- Connection strings (if using database features)
- Application Insights instrumentation key
- Compilation debug mode

### IIS Express Configuration

Port and binding settings in `.vs/config/applicationhost.config`:
- Default port: 51564
- Protocol: HTTP

## Development

### Build Commands

**Debug Build:**
```bash
msbuild SimplifiedDipsticksCalc.sln /p:Configuration=Debug
```

**Release Build:**
```bash
msbuild SimplifiedDipsticksCalc.sln /p:Configuration=Release
```

### Code Style

- **Option Strict:** OFF (legacy codebase)
- **Language:** VB.NET with auto-property syntax
- **Naming:** PascalCase for properties and methods
- **Documentation:** XML documentation generated during build

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Testing

Manual testing checklist:
- [ ] Each tank type calculates correctly
- [ ] All unit systems produce accurate results
- [ ] RegDip and Workshop modes both work
- [ ] CSV export generates valid files
- [ ] Session persistence maintains form data
- [ ] Tilt calculations for rectangular tanks
- [ ] Edge cases (zero increments, maximum volumes)

## Known Issues

See [bugs.md](bugs.md) for a comprehensive list of known issues and their status.

## License

[Add your license information here]

## Contact

Project Link: [https://github.com/steveminnikin/SimplifiedDipsticksCalc](https://github.com/steveminnikin/SimplifiedDipsticksCalc)

## Acknowledgments

- Built with ASP.NET MVC 5
- UI components from Bootstrap
- JavaScript functionality with jQuery
- Tank calculation algorithms based on industry-standard geometric formulas
