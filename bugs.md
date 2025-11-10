# Bugs and Issues - SimplifiedDipsticksCalc

**Last Updated:** 2025-11-09
**Total Issues Found:** 22
**Fixed:** 13

## Summary

| Severity | Count | Fixed |
|----------|-------|-------|
| Critical | 3 | 3 ✅ |
| High | 6 | 6 ✅ |
| Moderate | 4 | 4 ✅ |
| Low-Moderate | 4 | 0 |
| Low | 5 | 0 |

---

## CRITICAL ISSUES

### 1. ✅ Loop Condition Logic Error in EllipticalService.vb

**File:** `SimplifiedDipsticksCalc/Services/EllipticalService.vb:47-50`
**Status:** ✅ Fixed
**Severity:** Critical

**Issue:**
```vb
Do
    T9 = TN
    xFactors(elliptical)
Loop Until Abs(TN - T9) >= 0.00001
```

The loop condition is inverted. `Loop Until` should have `<= 0.00001` (less than or equal to threshold), not `>= 0.00001`. Currently, the loop will exit when the difference is LARGE, not when it's SMALL. This defeats the purpose of a convergence loop that should terminate when values become very close.

**Impact:** Incorrect calculations for elliptical tanks, convergence algorithm fails

**Suggested Fix:**
```vb
Loop Until Abs(TN - T9) <= 0.00001
```

---

### 2. ✅ Opposite Loop Condition in HorizFlatEndsService.vb

**File:** `SimplifiedDipsticksCalc/Services/HorizFlatEndsService.vb:43-46`
**Status:** ✅ Fixed
**Severity:** Critical

**Issue:**
```vb
Do
    T9 = TN
    xFactors(horizFlatEnds)
Loop While Abs(TN - T9) >= 0.00001
```

This uses `Loop While` instead of `Loop Until`, creating opposite semantics. The loop continues while the difference is >= 0.00001, which means it will loop while values are FAR APART and exit when they're close. This is logically equivalent to the buggy Elliptical version but using different syntax. The convergence threshold is backwards.

**Impact:** Incorrect calculations for horizontal flat-ended tanks, convergence algorithm fails

**Suggested Fix:**
```vb
Loop Until Abs(TN - T9) <= 0.00001
```

---

### 3. ✅ Uninitialized Structure in TankService.vb

**File:** `SimplifiedDipsticksCalc/Services/TankService.vb:12-48`
**Status:** ✅ Fixed
**Severity:** Critical

**Issue:**
```vb
Function GetinitialConversionValues(tank As Tank) As IInitialConversionValues
    Dim initialConversionValues As IInitialConversionValues

    With initialConversionValues
        Select Case tank.Dimensions
            ' ... assignments
        End Select
    End With

    Return initialConversionValues
End Function
```

The structure `initialConversionValues` is declared but never initialized. While VB.NET value types are zero-initialized, it's poor practice and semantically incorrect. The `With` statement operates on an uninitialized value type.

**Impact:** Unpredictable behavior, potential runtime errors

**Suggested Fix:**
```vb
Dim initialConversionValues As New IInitialConversionValues()
```

---

## HIGH SEVERITY ISSUES

### 4. ✅ Potential Division by Zero in HorizFlatEndsService.vb

**File:** `SimplifiedDipsticksCalc/Services/HorizFlatEndsService.vb:57-59`
**Status:** ✅ Fixed
**Severity:** High

**Issue:**
```vb
Sub XFactors(horizFlatEnds As HorizFlatEnds)
    X = T9 - Sin(T9) - (2 * horizFlatEnds.InitialConversionValues.cor * TIV / (R ^ 2 * convertedHorizFlatEndsDimensions.l))
    X = X / (1 - Cos(T9))  ' Division by zero when T9 = 0 or multiples of 2*PI
    TN = T9 - X
End Sub
```

Line 58 divides by `(1 - Cos(T9))`. When T9 = 0 (initial value) or at multiples of 2*PI, Cos(T9) = 1, making the denominator = 0, causing a division by zero error.

**Impact:** Runtime crash (DivideByZeroException)

**Suggested Fix:**
```vb
If Abs(1 - Cos(T9)) < 0.00001 Then
    ' Handle or skip this iteration
Else
    X = X / (1 - Cos(T9))
End If
```

---

### 5. ✅ Potential Division by Zero in EllipticalService.vb

**File:** `SimplifiedDipsticksCalc/Services/EllipticalService.vb:60-63`
**Status:** ✅ Fixed
**Severity:** High

**Issue:**
```vb
Sub XFactors(elliptical As Elliptical)
    X = T9 - Sin(T9) - (2 * elliptical.InitialConversionValues.cor * TIV / (R ^ 2 * convertedEllipticalDimensions.len))
    X = X / (1 - Cos(T9))  ' Division by zero when T9 = 0
    TN = T9 - X
End Sub
```

Same issue as HorizFlatEndsService - line 62 divides by `(1 - Cos(T9))` without validation.

**Impact:** Runtime crash (DivideByZeroException)

**Suggested Fix:** Add division by zero guard (see issue #4)

---

### 6. ✅ Potential Division by Zero in HorizDishEndsService.vb (VolCalcs)

**File:** `SimplifiedDipsticksCalc/Services/HorizDishEndsService.vb:167-174`
**Status:** ✅ Fixed
**Severity:** High

**Issue:**
```vb
Private Function VolCalcs(horizDishEnds As HorizDishEnds) As Double
    c6 = y2 * Cos(a) / (3 * Sin(a))  ' Division by zero when Sin(a) = 0
    vol = volcor * (c3 - c4 + c5 + c6) / horizDishEnds.InitialConversionValues.cor
    Return vol
End Function
```

Line 172 divides by `(3 * Sin(a))`. When a = 0 or multiples of PI, Sin(a) = 0, causing division by zero. Also line 173 could divide by zero if `horizDishEnds.InitialConversionValues.cor` = 0.

**Impact:** Runtime crash (DivideByZeroException)

**Suggested Fix:**
```vb
If Abs(Sin(a)) < 0.00001 OrElse horizDishEnds.InitialConversionValues.cor = 0 Then
    Throw New InvalidOperationException("Invalid parameters for volume calculation")
End If
```

---

### 7. ✅ Potential Division by Zero in HorizDishEndsService.vb (BasicConstants)

**File:** `SimplifiedDipsticksCalc/Services/HorizDishEndsService.vb:176-196`
**Status:** ✅ Fixed
**Severity:** High

**Issue:**
```vb
Protected Friend Sub BasicConstants()
    ht = convertedHorizDishEndsDimensions.dia / Cos(t2)  ' Division by zero when Cos(t2) = 0
```

Line 179 divides by `Cos(t2)` without checking if it's zero.

**Impact:** Runtime crash (DivideByZeroException)

**Suggested Fix:**
```vb
If Abs(Cos(t2)) < 0.00001 Then
    Throw New InvalidOperationException("Invalid angle for calculation")
End If
```

---

### 8. ✅ Potential Division by Zero in RectangularService.vb

**File:** `SimplifiedDipsticksCalc/Services/RectangularService.vb:108-129`
**Status:** ✅ Fixed
**Severity:** High

**Issue:**
```vb
For i = i To tv Step i
    d = (((Sqrt(i * til * 2 / (l * w))) * 10) - (til * dp / l))
```

Line 111 has division by `(l * w)` and by `l`. If either l or w is 0, division by zero occurs. No validation that tank dimensions are positive.

**Impact:** Runtime crash (DivideByZeroException)

**Suggested Fix:**
```vb
If l <= 0 OrElse w <= 0 Then
    Throw New ArgumentException("Length and width must be positive")
End If
```

---

### 9. ✅ Potential NullReferenceException in TankService.vb

**File:** `SimplifiedDipsticksCalc/Services/TankService.vb:52-70`
**Status:** ✅ Fixed
**Severity:** High

**Issue:**
```vb
Sub DownloadEngraveCode(tank As Tank)
    Dim fileName As String = "FV " + Round(tank.FullVol).ToString + "_INCS " + tank.Increments.ToString + tank.Details + ".csv"
    ' ...
    With HttpContext.Current.Response
        ' ...
    End With
End Sub
```

`HttpContext.Current` can be null if the function is called outside of an HTTP context. Additionally, `tank.Details` could be null, causing a NullReferenceException when concatenating strings.

**Impact:** Runtime crash (NullReferenceException)

**Suggested Fix:**
```vb
If HttpContext.Current Is Nothing Then
    Throw New InvalidOperationException("This method must be called within an HTTP context")
End If
If tank.Details Is Nothing Then tank.Details = ""
```

---

## MODERATE SEVERITY ISSUES

### 10. ✅ Potential Dictionary Key Duplicate Issues

**File:** Multiple service classes (e.g., `SimplifiedDipsticksCalc/Services/RectangularService.vb:14-41`)
**Status:** ✅ Fixed
**Severity:** Moderate

**Issue:**

The `FinalConversionRounding()` function can produce the same value for different heights/volumes, causing duplicate keys when adding to `incrementList` dictionary:
```vb
incrementList.Add(rectangular.FinalConversionRounding(h), Round(iv))
```

For example, if `FinalConversionRounding()` rounds 1.45 and 1.54 to the same value, attempting to add both will fail with "An item with the same key has already been added."

**Impact:** Runtime crash (ArgumentException) when duplicate rounded values occur

**Suggested Fix:**
```vb
Dim key = rectangular.FinalConversionRounding(h)
If Not incrementList.ContainsKey(key) Then
    incrementList.Add(key, Round(iv))
End If
```

---

### 11. ✅ Type Conversion Issue - Case Sensitivity in Property Names

**File:** `SimplifiedDipsticksCalc/Services/HorizFlatEndsService.vb:62`
**Status:** ✅ Fixed
**Severity:** Moderate

**Issue:**
```vb
Function GetConvertedHorizFlatEndsDimensions(horizFlatEnds As HorizFlatEnds) As IConvertedFLatEndsDimensions
```

The return type is `IConvertedFLatEndsDimensions` (with capital "F" and "Lat") but the structure definition is `IConvertedFlatEndsDimensions` (lowercase "lat"). Inconsistent type naming.

**Impact:** Code confusion, potential type mismatch issues

**Suggested Fix:** Standardize naming to `IConvertedFlatEndsDimensions` everywhere

---

### 12. ✅ Inconsistent Property Naming - Case Mismatch

**File:** Multiple service files
**Status:** ✅ Fixed
**Severity:** Moderate

**Issue:**

Property naming inconsistencies throughout the codebase:
- `horizDishEnds.regDip` vs `horizDishEnds.RegDip` (HorizDishEndsService.vb:49, 57)
- `vertCyl.regDip` vs `vertCyl.RegDip` (VertCylService.vb:15)
- `elliptical.regDip` vs `elliptical.RegDip` (EllipticalService.vb:25)

**Impact:** Code readability, potential maintenance issues

**Suggested Fix:** Adopt consistent PascalCase naming convention: `RegDip`

---

### 13. ✅ Potential Negative Volume in VertCylService.vb

**File:** `SimplifiedDipsticksCalc/Services/VertCylService.vb:13,17`
**Status:** ✅ Fixed
**Severity:** Moderate

**Issue:**
```vb
vertCyl.FullVol = (area * convertedVertDimensions.ht / vertCyl.InitialConversionValues.cor) - vertCyl.Adjustments
iv = area * (h / vertCyl.InitialConversionValues.cor) - vertCyl.Adjustments
```

If `Adjustments` is larger than the calculated volume, you get negative volumes, which are physically meaningless.

**Impact:** Nonsensical negative volume values in output

**Suggested Fix:**
```vb
If vertCyl.Adjustments > vertCyl.FullVol Then
    Throw New InvalidOperationException("Adjustments cannot exceed full volume")
End If
```

---

## LOW-MODERATE SEVERITY ISSUES

### 14. ❌ String Concatenation XSS Risk in JavaScript

**File:** `SimplifiedDipsticksCalc/Scripts/custom.js:97,101,105,109,113,117,134`
**Status:** 🔴 Open
**Severity:** Low-Moderate

**Issue:**

While the code does use `.text()` which provides some XSS protection, the approach is inconsistent:
```javascript
$clientData.append($('<span>').text(retrievedClient.Name)).append('<br />');
```

The code mixes `.text()` with raw HTML strings.

**Impact:** Potential XSS vulnerability if user input is not properly escaped

**Suggested Fix:**
```javascript
$clientData.append($('<br>'));  // Instead of .append('<br />')
```

---

### 15. ❌ Missing Input Validation in Controllers

**File:** All controllers (e.g., `SimplifiedDipsticksCalc/Controllers/RectangularController.vb:25-41`)
**Status:** 🔴 Open
**Severity:** Low-Moderate

**Issue:**

Controllers receive form data but don't validate that dimensions are positive:
```vb
Function Calculate(<Bind(Include:="Length,Width,Height,...")> rectangular As Rectangular) As ActionResult
    ' No validation that Length, Width, Height > 0
```

Zero or negative dimensions lead to meaningless calculations and division by zero errors.

**Impact:** Invalid calculations, potential runtime errors

**Suggested Fix:**
```vb
If rectangular.Length <= 0 OrElse rectangular.Width <= 0 OrElse rectangular.Height <= 0 Then
    ModelState.AddModelError("", "All dimensions must be positive numbers")
    Return View(rectangular)
End If
```

---

### 16. ❌ Missing Null Check in HorizDishEndsService.vb

**File:** `SimplifiedDipsticksCalc/Services/HorizDishEndsService.vb:165-174`
**Status:** 🔴 Open
**Severity:** Low-Moderate

**Issue:**
```vb
Private Function VolCalcs(horizDishEnds As HorizDishEnds) As Double
    vol = volcor * (c3 - c4 + c5 + c6) / horizDishEnds.InitialConversionValues.cor
```

No null check for `InitialConversionValues` or validation that `cor` is not zero.

**Impact:** Potential NullReferenceException or division by zero

**Suggested Fix:** Add null and zero checks at the beginning of the method

---

### 17. ❌ Large Commented-Out Code Block in RectangularService.vb

**File:** `SimplifiedDipsticksCalc/Services/RectangularService.vb:89-91,130-135`
**Status:** 🔴 Open
**Severity:** Low-Moderate

**Issue:**
```vb
'h = (h - dbdb) * 100
'vol = (l * w * h) - vtilt
'dipTable.Rows.Add(Round(vol), mark)

' ... and later:
'adjustment to convert output volume to US Gallons
```

Dead code should be removed, not left commented.

**Impact:** Code maintainability and readability

**Suggested Fix:** Remove commented-out code or use version control for history

---

## LOW SEVERITY ISSUES

### 18. ❌ Misleading Display Name in Elliptical.vb

**File:** `SimplifiedDipsticksCalc/Models/Elliptical.vb:8,11`
**Status:** 🔴 Open
**Severity:** Low

**Issue:**
```vb
<Display(Name:=" MajorDiameter")>  ' Extra space before name
Property MajorDiameter As Double
<Display(Name:=" MinorDiameter")>  ' Extra space before name
Property MinorDiameter As Double
```

Display names have leading spaces, which will show in the UI with visual padding.

**Impact:** UI formatting issues

**Suggested Fix:**
```vb
<Display(Name:="Major Diameter")>
<Display(Name:="Minor Diameter")>
```

---

### 19. ❌ Empty Case Statement in TankService.vb

**File:** `SimplifiedDipsticksCalc/Services/TankService.vb:43-44`
**Status:** 🔴 Open
**Severity:** Low

**Issue:**
```vb
Case Else
    ' No action for unexpected Dimension value
End Select
```

The Case Else is empty. If an unexpected Dimension type is passed, the function returns an uninitialized structure.

**Impact:** Silent failure with unexpected dimension types

**Suggested Fix:**
```vb
Case Else
    Throw New ArgumentException("Unknown dimension type: " & tank.Dimensions.ToString())
```

---

### 20. ❌ Goto Statements in HorizDishEndsService.vb

**File:** `SimplifiedDipsticksCalc/Services/HorizDishEndsService.vb:52-97`
**Status:** 🔴 Open
**Severity:** Low

**Issue:**
```vb
Volcalcs:   VolCalcs(horizDishEnds)
            If init = False Then GoTo 6740
            If swit = True Then GoTo 6760
            ' ... many GoTo statements with numeric labels
6640:       If horizDishEnds.regDip = True Then
6740:       init = True
6760:       If vol > xinc And ai < 0.001 Then GoTo 6920
```

Use of `GoTo` with numeric labels is a code smell. Makes code harder to follow and maintain.

**Impact:** Code maintainability

**Suggested Fix:** Refactor using proper loops and methods instead of GoTo

---

### 21. ❌ Public Field Instead of Property in Model Classes

**File:** Multiple model classes
**Status:** 🔴 Open
**Severity:** Low

**Issue:**

Several model classes expose fields directly:
```vb
Public convertedVertDimensions As IConvertedVertDimensions  ' VertCyl.vb:12
Public convertedFlatEndsDimensions As IConvertedFLatEndsDimensions  ' HorizFlatEnds.vb:14
Public convertedEllipticalDimensions As IConvertedEllipticalDimensions  ' Elliptical.vb:17
```

Should be properties for encapsulation.

**Impact:** Encapsulation best practices

**Suggested Fix:**
```vb
Public Property ConvertedVertDimensions As IConvertedVertDimensions
```

---

### 22. ❌ Missing Null Check in Custom.js SessionStorage

**File:** `SimplifiedDipsticksCalc/Scripts/custom.js:88-91`
**Status:** 🔴 Open
**Severity:** Low

**Issue:**
```javascript
var retrievedClient = JSON.parse(sessionStorage.getItem('Client'));
if (!retrievedClient) {
    return;
}
// retrievedClient.Name is used without additional null checks
```

Good check exists for retrievedClient, but could be more defensive.

**Impact:** Minor - already has basic null check

**Suggested Fix:** Continue current pattern of null checks before accessing properties

---

## Priority Recommendations

1. **Immediate action required** - Fix the 3 critical loop condition bugs that break core calculations
2. **High priority** - Add division by zero guards to all mathematical calculations
3. **Moderate priority** - Add input validation at controller level for all user inputs
4. **Low priority** - Clean up code style issues, remove dead code, refactor GoTo statements

---

## Notes

- Total lines reviewed: ~2000+
- Primary language: VB.NET (ASP.NET MVC 5)
- Framework: .NET Framework 4.5.2
- Option Strict: OFF (increases risk of type conversion issues)
