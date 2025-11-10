// Form Validation Module
var FormValidator = (function() {
    function isNumeric(value) {
        return !isNaN(parseFloat(value)) && isFinite(value);
    }

    function isPositive(value) {
        return isNumeric(value) && parseFloat(value) > 0;
    }

    function showError($field, message) {
        clearError($field);
        $field.addClass('has-error');
        $field.after('<span class="help-block text-danger validation-error">' + message + '</span>');
    }

    function clearError($field) {
        $field.removeClass('has-error');
        $field.next('.validation-error').remove();
    }

    function clearAllErrors() {
        $('.has-error').removeClass('has-error');
        $('.validation-error').remove();
    }

    function validateNumericField($field, fieldName, required) {
        var value = $field.val().trim();

        if (required && value === '') {
            showError($field, fieldName + ' is required.');
            return false;
        }

        if (value !== '' && !isNumeric(value)) {
            showError($field, fieldName + ' must be a valid number.');
            return false;
        }

        if (value !== '' && !isPositive(value)) {
            showError($field, fieldName + ' must be greater than zero.');
            return false;
        }

        clearError($field);
        return true;
    }

    function validateDimensionalFields() {
        clearAllErrors();
        var isValid = true;
        var activeTab = $('.tab-pane.active').attr('id');

        // Get active tank type
        var requiredFields = [];

        if (activeTab === 'rectangular') {
            requiredFields = [
                { selector: '#Length', name: 'Length' },
                { selector: '#Width', name: 'Width' },
                { selector: '#Height', name: 'Height' }
            ];
        } else if (activeTab === 'vertCyl') {
            requiredFields = [
                { selector: '#Diameter', name: 'Diameter' },
                { selector: '#Height', name: 'Height' }
            ];
        } else if (activeTab === 'horizFlatEnds') {
            requiredFields = [
                { selector: '#Diameter', name: 'Diameter' },
                { selector: '#Length', name: 'Length' }
            ];
        } else if (activeTab === 'horizDishEnds') {
            requiredFields = [
                { selector: '#Diameter', name: 'Diameter' },
                { selector: '#Length', name: 'Length' },
                { selector: '#DishEndRadius', name: 'Dished End Radius' }
            ];
        } else if (activeTab === 'ellipt') {
            requiredFields = [
                { selector: '#MajorAxis', name: 'Major Axis' },
                { selector: '#MinorAxis', name: 'Minor Axis' },
                { selector: '#Length', name: 'Length' }
            ];
        }

        // Validate required fields
        requiredFields.forEach(function(field) {
            var $field = $(field.selector);
            if ($field.length) {
                if (!validateNumericField($field, field.name, true)) {
                    isValid = false;
                }
            }
        });

        // Validate Increments field (always required)
        var $increments = $('#incrementsInput');
        if ($increments.length && !validateNumericField($increments, 'Increments', true)) {
            isValid = false;
        }

        // Validate Adjustments if filled (optional but must be numeric if provided)
        var $adjustments = $('#Adjustments');
        if ($adjustments.length && $adjustments.val().trim() !== '') {
            if (!validateNumericField($adjustments, 'Adjustments', false)) {
                isValid = false;
            }
        }

        // Dimensional relationship validations
        if (activeTab === 'ellipt') {
            var majorAxis = parseFloat($('#MajorAxis').val());
            var minorAxis = parseFloat($('#MinorAxis').val());
            if (!isNaN(majorAxis) && !isNaN(minorAxis) && minorAxis > majorAxis) {
                showError($('#MinorAxis'), 'Minor Axis must be less than or equal to Major Axis.');
                isValid = false;
            }
        }

        return isValid;
    }

    return {
        validate: validateDimensionalFields,
        clearErrors: clearAllErrors,
        isNumeric: isNumeric,
        isPositive: isPositive
    };
})();

// Configuration Export/Import Module
var ConfigManager = (function() {
    function getCurrentConfiguration() {
        var activeTab = $('.tab-pane.active').attr('id');
        var tankTypeMap = {
            'horizDishEnds': 'Horizontal Cylindrical Dished Ends',
            'horizFlatEnds': 'Horizontal Cylindrical Flat Ends',
            'rectangular': 'Rectangular',
            'vertCyl': 'Vertical Cylindrical',
            'ellipt': 'Elliptical'
        };

        var config = {
            version: '1.0',
            exportDate: new Date().toISOString(),
            tankType: tankTypeMap[activeTab],
            client: {
                Name: $('#Name').val(),
                Ref: $('#Ref').val(),
                Notes: $('#Notes').val(),
                Date: $('#Date').val(),
                TankRef: $('#tankRef').val(),
                OurRef: $('#ourRef').val()
            },
            settings: {
                Dimensions: $('#Dimensions').val(),
                RegDip: $('input[name="regDip"]:checked').val(),
                EngraveCode: $('#EngraveCode').is(':checked'),
                Adjustments: $('#Adjustments').val(),
                Increments: $('#incrementsInput').val()
            },
            tankData: {}
        };

        // Collect all input values from active tab
        $('.tab-pane.active input[type="text"], .tab-pane.active input[type="number"]').each(function() {
            var $input = $(this);
            var id = $input.attr('id');
            var value = $input.val();
            if (id && value) {
                config.tankData[id] = value;
            }
        });

        return config;
    }

    function exportConfiguration() {
        try {
            var config = getCurrentConfiguration();
            var json = JSON.stringify(config, null, 2);
            var blob = new Blob([json], { type: 'application/json' });
            var url = URL.createObjectURL(blob);

            var filename = 'tank-config-' + config.tankType.replace(/\s+/g, '-').toLowerCase() + '-' +
                          new Date().toISOString().slice(0, 10) + '.json';

            var a = document.createElement('a');
            a.href = url;
            a.download = filename;
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
            URL.revokeObjectURL(url);

            return true;
        } catch (e) {
            console.error('Failed to export configuration:', e);
            alert('Failed to export configuration. ' + e.message);
            return false;
        }
    }

    function importConfiguration(fileInput) {
        var file = fileInput.files[0];
        if (!file) {
            return;
        }

        var reader = new FileReader();
        reader.onload = function(e) {
            try {
                var config = JSON.parse(e.target.result);
                applyConfiguration(config);
                alert('Configuration imported successfully!');
                fileInput.value = ''; // Clear file input
            } catch (error) {
                console.error('Failed to import configuration:', error);
                alert('Failed to import configuration. Please ensure the file is a valid tank configuration file.');
            }
        };
        reader.readAsText(file);
    }

    function applyConfiguration(config) {
        if (!config || !config.tankType) {
            throw new Error('Invalid configuration file');
        }

        // Switch to correct tab
        var tabMap = {
            'Horizontal Cylindrical Dished Ends': '#horizDishEnds',
            'Horizontal Cylindrical Flat Ends': '#horizFlatEnds',
            'Rectangular': '#rectangular',
            'Vertical Cylindrical': '#vertCyl',
            'Elliptical': '#ellipt'
        };

        var tabId = tabMap[config.tankType];
        if (tabId) {
            $('a[href="' + tabId + '"]').tab('show');
        }

        // Apply client information
        if (config.client) {
            $('#Name').val(config.client.Name || '');
            $('#Ref').val(config.client.Ref || '');
            $('#Notes').val(config.client.Notes || '');
            $('#Date').val(config.client.Date || '');
            $('#tankRef').val(config.client.TankRef || '');
            $('#ourRef').val(config.client.OurRef || '');
        }

        // Apply settings
        if (config.settings) {
            if (config.settings.Dimensions) {
                $('#Dimensions').val(config.settings.Dimensions);
            }
            if (config.settings.RegDip !== undefined) {
                $('input[name="regDip"][value="' + config.settings.RegDip + '"]').prop('checked', true);
            }
            if (config.settings.EngraveCode !== undefined) {
                $('#EngraveCode').prop('checked', config.settings.EngraveCode);
            }
            if (config.settings.Adjustments) {
                $('#Adjustments').val(config.settings.Adjustments);
            }
            if (config.settings.Increments) {
                $('#incrementsInput').val(config.settings.Increments);
            }
        }

        // Apply tank-specific data
        if (config.tankData) {
            for (var key in config.tankData) {
                if (config.tankData.hasOwnProperty(key)) {
                    $('#' + key).val(config.tankData[key]);
                }
            }
        }
    }

    return {
        export: exportConfiguration,
        import: importConfiguration,
        getCurrent: getCurrentConfiguration
    };
})();

// Calculation History Management
var CalculationHistory = (function() {
    var MAX_HISTORY_ITEMS = 10;
    var STORAGE_KEY = 'dipstickCalculationHistory';

    function saveCalculation(tankType, formData, client) {
        try {
            var history = getHistory();
            var calculation = {
                id: Date.now(),
                timestamp: new Date().toISOString(),
                tankType: tankType,
                formData: formData,
                client: client
            };

            history.unshift(calculation);
            if (history.length > MAX_HISTORY_ITEMS) {
                history = history.slice(0, MAX_HISTORY_ITEMS);
            }

            localStorage.setItem(STORAGE_KEY, JSON.stringify(history));
            return true;
        } catch (e) {
            console.error('Failed to save calculation to history:', e);
            return false;
        }
    }

    function getHistory() {
        try {
            var history = localStorage.getItem(STORAGE_KEY);
            return history ? JSON.parse(history) : [];
        } catch (e) {
            console.error('Failed to retrieve calculation history:', e);
            return [];
        }
    }

    function clearHistory() {
        try {
            localStorage.removeItem(STORAGE_KEY);
            return true;
        } catch (e) {
            console.error('Failed to clear calculation history:', e);
            return false;
        }
    }

    function deleteCalculation(id) {
        try {
            var history = getHistory();
            history = history.filter(function(calc) { return calc.id !== id; });
            localStorage.setItem(STORAGE_KEY, JSON.stringify(history));
            return true;
        } catch (e) {
            console.error('Failed to delete calculation:', e);
            return false;
        }
    }

    return {
        save: saveCalculation,
        getAll: getHistory,
        clear: clearHistory,
        delete: deleteCalculation
    };
})();

$(document).ready(function () {
    var count = 0;
    var Data = [];
    var retrievedData = [];

    // Initialize calculation history dropdown
    function initCalculationHistory() {
        var history = CalculationHistory.getAll();
        var $dropdown = $('#historyDropdown');

        if (!$dropdown.length || history.length === 0) {
            return;
        }

        $dropdown.empty();
        $dropdown.append('<option value="">-- Select Previous Calculation --</option>');

        history.forEach(function(calc) {
            var date = new Date(calc.timestamp);
            var dateStr = date.toLocaleDateString() + ' ' + date.toLocaleTimeString();
            var label = calc.tankType + ' - ' + dateStr;
            if (calc.client && calc.client.Name) {
                label += ' (' + calc.client.Name + ')';
            }
            $dropdown.append('<option value="' + calc.id + '">' + label + '</option>');
        });
    }

    // Load a calculation from history
    function loadCalculationFromHistory(calculationId) {
        var history = CalculationHistory.getAll();
        var calculation = history.find(function(calc) { return calc.id === parseInt(calculationId); });

        if (!calculation) {
            return;
        }

        // Switch to the correct tab
        var tabMap = {
            'Horizontal Cylindrical Dished Ends': '#horizDishEnds',
            'Horizontal Cylindrical Flat Ends': '#horizFlatEnds',
            'Rectangular': '#rectangular',
            'Vertical Cylindrical': '#vertCyl',
            'Elliptical': '#ellipt'
        };

        var tabId = tabMap[calculation.tankType];
        if (tabId) {
            $('a[href="' + tabId + '"]').tab('show');
        }

        // Populate client fields
        if (calculation.client) {
            $('#Name').val(calculation.client.Name || '');
            $('#Ref').val(calculation.client.Ref || '');
            $('#Notes').val(calculation.client.Notes || '');
            $('#Date').val(calculation.client.Date || '');
            $('#tankRef').val(calculation.client.TankRef || '');
            $('#ourRef').val(calculation.client.OurRef || '');
        }

        // Populate form fields
        if (calculation.formData) {
            calculation.formData.forEach(function(field) {
                var $input = $('#' + field.id);
                if ($input.length) {
                    $input.val(field.value);
                }
            });
        }
    }

    // Clear calculation history
    function clearCalculationHistory() {
        if (confirm('Are you sure you want to clear all calculation history? This cannot be undone.')) {
            CalculationHistory.clear();
            initCalculationHistory();
            alert('Calculation history has been cleared.');
        }
    }

    // Handle history dropdown change
    $(document).on('change', '#historyDropdown', function() {
        var calculationId = $(this).val();
        if (calculationId) {
            loadCalculationFromHistory(calculationId);
        }
    });

    // Handle clear history button
    $(document).on('click', '#btnClearHistory', function() {
        clearCalculationHistory();
    });

    // Handle export configuration button
    $(document).on('click', '#btnExportConfig', function() {
        ConfigManager.export();
    });

    // Handle import configuration button
    $(document).on('change', '#fileImportConfig', function() {
        ConfigManager.import(this);
    });

    $(document).on('click', '#btnImportConfig', function() {
        $('#fileImportConfig').click();
    });

    // Initialize history on page load
    initCalculationHistory();

    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
        sessionStorage.setItem('activeTab', $(e.target).attr('href'));
    });
    var activeTab = sessionStorage.getItem('activeTab');
    if (activeTab) {
        $('a[href="' + activeTab + '"]').tab('show');
    }

    if (!sessionStorage.getItem("hasCodeRunBefore")) {
        try {
            sessionStorage.setItem("hasCodeRunBefore", true);
        } catch (e) {
            console.error('SessionStorage not available:', e);
        }
    }
    else {
        try {
            $('form input').each(function (i, e) {
                retrievedData = JSON.parse(sessionStorage.getItem('Data'));
                if (retrievedData && retrievedData[i]) {
                    e.value = retrievedData[i].value;
                }
            });
        } catch (e) {
            console.error('Failed to restore form data:', e);
        }
    }

    $('.btnSubmit').click(function (e) {
        // Validate form before submission
        if (!FormValidator.validate()) {
            e.preventDefault();

            // Scroll to first error
            var $firstError = $('.has-error').first();
            if ($firstError.length) {
                $('html, body').animate({
                    scrollTop: $firstError.offset().top - 100
                }, 500);
            }

            alert('Please correct the validation errors before submitting.');
            return false;
        }

        var Client = {
            Name: $('#Name').val(),
            Ref: $('#Ref').val(),
            Notes: $('#Notes').val(),
            Date: $('#Date').val(),
            TankRef: $('#tankRef').val(),
            OurRef: $('#ourRef').val()
        };

        $('form input').each(function (i, e) {
            Data[i] = { id: e.id, value: e.value };
        });

        try {
            sessionStorage.setItem('Data', JSON.stringify(Data));
            sessionStorage.setItem('Client', JSON.stringify(Client));
        } catch (e) {
            console.error('Failed to save form data:', e);
            alert('Could not save form data. Your browser may have cookies/storage disabled.');
        }

        // Determine tank type and save to calculation history
        var tankType = '';
        //set the post action on the tab form
        if ($('#horizDishEnds').hasClass('active')) {
            $('#submitForm').attr('action', '/HorizDishEnds/Calculate');
            tankType = 'Horizontal Cylindrical Dished Ends';
        }
        if ($('#horizFlatEnds').hasClass('active')) {
            $('#submitForm').attr('action', '/HorizFlatEnds/Calculate');
            tankType = 'Horizontal Cylindrical Flat Ends';
        }
        if ($('#rectangular').hasClass('active')) {
            $('#submitForm').attr('action', '/Rectangular/Calculate');
            tankType = 'Rectangular';
        }
        if ($('#vertCyl').hasClass('active')) {
            $('#submitForm').attr('action', '/VertCyl/Calculate');
            tankType = 'Vertical Cylindrical';
        }
        if ($('#ellipt').hasClass('active')) {
            $('#submitForm').attr('action', '/Elliptical/Calculate');
            tankType = 'Elliptical';
        }

        // Save calculation to history
        CalculationHistory.save(tankType, Data, Client);

    }); //btnSubmit.click  

    $('#btnEdit').click(function () {

        if (isEven(count)) {
            document.body.contentEditable = true;
        }
        if (!isEven(count)) {
            document.body.contentEditable = false;
        }
        count += 1;
    });

    $('#btnClient').click(function () {
        try {
            var retrievedClient = JSON.parse(sessionStorage.getItem('Client'));
            if (!retrievedClient) {
                return;
            }

            var $clientTitle = $('#clientTitle');
            var $clientData = $('#clientData');
            if (retrievedClient.Name) {
                $clientTitle.append($('<dt>').text('Client'));
                $clientData.append($('<span>').text(retrievedClient.Name)).append('<br />');
            }
            if (retrievedClient.Ref) {
                $clientTitle.append($('<dt>').text('Ref'));
                $clientData.append($('<span>').text(retrievedClient.Ref)).append('<br />');
            }
            if (retrievedClient.Notes) {
                $clientTitle.append($('<dt>').text('Notes'));
                $clientData.append($('<span>').text(retrievedClient.Notes)).append('<br />');
            }

            $clientTitle.append($('<dt>').text('Date'));
            $clientData.append($('<span>').text(retrievedClient.Date)).append('<br />');

            if (retrievedClient.TankRef) {
                $clientTitle.append($('<dt>').text('Tank Ref'));
                $clientData.append($('<span>').text(retrievedClient.TankRef)).append('<br />');
            }
            if (retrievedClient.OurRef) {
                $clientTitle.append($('<dt>').text('Chart No'));
                $clientData.append($('<span>').text(retrievedClient.OurRef)).append('<br />');
            }

            //Toggle display of the client info on the screen
            var $clientInfo = $('#clientInfo');
            $clientInfo.toggleClass('visible-print');
        } catch (e) {
            console.error('Failed to display client information:', e);
        }
    });

    var isEven = function (someNumber) {
        return someNumber % 2 === 0 ? true : false;
    };

    $('#btnNote').click(function () {
        var myNote = prompt("Add a note to the chart", "Add 6mm to dip reading before using chart to allow for striker plate");
        $('#chartNote').text(myNote);
    });
});

  
