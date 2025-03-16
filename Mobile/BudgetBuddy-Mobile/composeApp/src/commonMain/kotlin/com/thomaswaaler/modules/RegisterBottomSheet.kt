package com.thomaswaaler.modules

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.automirrored.filled.KeyboardArrowRight
import androidx.compose.material.icons.filled.Close
import androidx.compose.material3.Button
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.ExposedDropdownMenuBox
import androidx.compose.material3.ExposedDropdownMenuDefaults
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.MenuAnchorType
import androidx.compose.material3.ModalBottomSheet
import androidx.compose.material3.ModalBottomSheetProperties
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.SegmentedButton
import androidx.compose.material3.SegmentedButtonDefaults
import androidx.compose.material3.SheetValue
import androidx.compose.material3.SingleChoiceSegmentedButtonRow
import androidx.compose.material3.Text
import androidx.compose.material3.rememberModalBottomSheetState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.TextFieldValue
import androidx.compose.ui.unit.dp
import budgetbuddy.composeapp.generated.resources.Res
import budgetbuddy.composeapp.generated.resources.trending_down_24dp
import budgetbuddy.composeapp.generated.resources.trending_up_24dp
import kotlinx.datetime.Clock
import org.jetbrains.compose.resources.vectorResource

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun RegisterBottomSheet(
    onDismiss: () -> Unit
) {
    val sheetState = rememberModalBottomSheetState(
        skipPartiallyExpanded = true,
        confirmValueChange = { it != SheetValue.Hidden }
    )
    var closingSheet by remember { mutableStateOf(false) }

    LaunchedEffect(closingSheet) {
        sheetState.hide()
        onDismiss()
    }

    val amountSignOptions: List<String> = listOf("Income", "Expense")
    val typeOptions: List<String> = listOf("Category", "Account Transfer", "Balance Adjustment")
    val categoryOptions: List<String> = listOf("Category 1", "Category 2", "Category 3")
    val accountOptions: List<String> = listOf("Account 1", "Account 2", "Account 3")

    // **** FORM DATA ****
    var selectedAmountSign by remember { mutableStateOf(1) }
    var textFieldValueState by remember { mutableStateOf(TextFieldValue(text = "")) }
    var datePickerValue by remember { mutableStateOf(Clock.System.now().toEpochMilliseconds()) }
    var typeOptionText by remember { mutableStateOf(typeOptions[0]) }
    var categoryOptionText by remember { mutableStateOf("") }
    var accountToOptionText by remember { mutableStateOf("") }
    var accountFromOptionText by remember { mutableStateOf("") }
    // ****           ****

    // **** FORM ERROR TYPES ****
    var amountErrorType by remember { mutableStateOf<CurrencyTextFieldError?>(null) }
    // ****                  ****

    var amountSignEnabled by remember { mutableStateOf(false) }

    var showCategoryDropdown by remember { mutableStateOf(true) }
    var showFromAccountDropdown by remember { mutableStateOf(true) }
    var showToAccountDropdown by remember { mutableStateOf(false) }

    fun clearDropdowns() {
        categoryOptionText = ""
        accountToOptionText = ""
        accountFromOptionText = ""
    }

    fun changedType() {
        when (typeOptionText) {
            typeOptions[0] -> {
                showCategoryDropdown = true
                showFromAccountDropdown = true
                showToAccountDropdown = false

                selectedAmountSign = 1
                amountSignEnabled = false
            }
            typeOptions[1] -> {
                showCategoryDropdown = false
                showFromAccountDropdown = true
                showToAccountDropdown = true

                selectedAmountSign = 0
                amountSignEnabled = false
            }
            typeOptions[2] -> {
                showCategoryDropdown = false
                showFromAccountDropdown = false
                showToAccountDropdown = true

                selectedAmountSign = 0
                amountSignEnabled = true
            }
        }

        clearDropdowns()
    }

    ModalBottomSheet(
        onDismissRequest = { closingSheet = true },
        sheetState = sheetState,
        properties = ModalBottomSheetProperties(false),
        dragHandle = {}
    ) {
        Column(
            modifier = Modifier
                .fillMaxHeight(0.95f)
                .padding(16.dp)
        ) {
            // Header
            Row(
                modifier = Modifier.fillMaxWidth(),
                verticalAlignment = Alignment.CenterVertically,
            ) {
                Text(
                    modifier = Modifier.weight(1F),
                    text = "Add Transaction",
                    style = MaterialTheme.typography.headlineSmall
                )
                IconButton(onClick = { closingSheet = true }) {
                    Icon(Icons.Filled.Close, contentDescription = "")
                }
            }

            // Content
            val scrollState = rememberScrollState()

            Column(
                verticalArrangement = Arrangement.spacedBy(8.dp),
                modifier = Modifier
                    .weight(1F)
                    .verticalScroll(scrollState)
            ) {
                // **** Amount Sign Selection ****
                SingleChoiceSegmentedButtonRow(
                    modifier = Modifier.fillMaxWidth()
                ) {
                    SegmentedButton(
                        shape = SegmentedButtonDefaults.itemShape(
                            index = 0,
                            count = amountSignOptions.size
                        ),
                        onClick = { selectedAmountSign = 0 },
                        selected = selectedAmountSign == 0,
                        label = { Text(amountSignOptions[0]) },
                        enabled = amountSignEnabled,
                        icon = { Icon(vectorResource(Res.drawable.trending_up_24dp), contentDescription = null) }
                    )
                    SegmentedButton(
                        shape = SegmentedButtonDefaults.itemShape(
                            index = 1,
                            count = amountSignOptions.size
                        ),
                        onClick = { selectedAmountSign = 1 },
                        selected = selectedAmountSign == 1,
                        label = { Text(amountSignOptions[1]) },
                        enabled = amountSignEnabled,
                        icon = { Icon(vectorResource(Res.drawable.trending_down_24dp), contentDescription = null) }
                    )
                }

                // **** Amount ****
                CurrencyTextField(
                    value = textFieldValueState,
                    onValueChange = { textFieldValueState = it },
                    errorType = amountErrorType,
                    onErrorTypeChanged = { amountErrorType = it },
                    modifier = Modifier.fillMaxWidth()
                )

                // **** Date ****
                DatePickerTextField(
                    value = datePickerValue,
                    onValueChanged = { datePickerValue = it },
                    modifier = Modifier.fillMaxWidth()
                )

                // **** Transaction Type ****
                var expanded by remember { mutableStateOf(false) }

                ExposedDropdownMenuBox(
                    expanded = expanded,
                    onExpandedChange = { expanded = !expanded }
                ) {
                    OutlinedTextField(
                        readOnly = true,
                        value = typeOptionText,
                        onValueChange = { },
                        label = { Text("Type") },
                        trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = expanded)},
                        modifier = Modifier
                            .menuAnchor(MenuAnchorType.PrimaryNotEditable, true)
                            .fillMaxWidth()
                    )

                    ExposedDropdownMenu(
                        expanded = expanded,
                        onDismissRequest = { expanded = false }
                    ) {
                        typeOptions.forEach { selectionOption ->
                            DropdownMenuItem(
                                text = { Text(selectionOption) },
                                onClick = {
                                    typeOptionText = selectionOption
                                    expanded = false
                                    changedType()
                                }
                            )
                        }
                    }
                }

                Spacer(modifier = Modifier.padding(top = 16.dp))

                // **** Category ****
                if (showCategoryDropdown) {
                    var categoryExpanded by remember { mutableStateOf(false) }

                    ExposedDropdownMenuBox(
                        expanded = categoryExpanded,
                        onExpandedChange = { categoryExpanded = !categoryExpanded }
                    ) {
                        OutlinedTextField(
                            readOnly = true,
                            value = categoryOptionText,
                            onValueChange = {},
                            label = { Text("Category") },
                            trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = categoryExpanded)},
                            modifier = Modifier
                                .menuAnchor(MenuAnchorType.PrimaryNotEditable, true)
                                .fillMaxWidth()
                        )

                        ExposedDropdownMenu(
                            expanded = categoryExpanded,
                            onDismissRequest = { categoryExpanded = false }
                        ) {
                            categoryOptions.forEach { selectionOption ->
                                DropdownMenuItem(
                                    text = { Text(selectionOption) },
                                    onClick = {
                                        categoryOptionText = selectionOption
                                        categoryExpanded = false
                                    }
                                )
                            }
                        }
                    }
                }

                // **** From Account ****
                if (showFromAccountDropdown) {
                    var accountFromExpanded by remember { mutableStateOf(false) }

                    ExposedDropdownMenuBox(
                        expanded = accountFromExpanded,
                        onExpandedChange = { accountFromExpanded = !accountFromExpanded }
                    ) {
                        OutlinedTextField(
                            readOnly = true,
                            value = accountFromOptionText,
                            onValueChange = {},
                            label = { Text("From Account") },
                            trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = accountFromExpanded)},
                            modifier = Modifier
                                .menuAnchor(MenuAnchorType.PrimaryNotEditable, true)
                                .fillMaxWidth()
                        )

                        ExposedDropdownMenu(
                            expanded = accountFromExpanded,
                            onDismissRequest = { accountFromExpanded = false }
                        ) {
                            accountOptions.forEach { selectionOption ->
                                DropdownMenuItem(
                                    text = { Text(selectionOption) },
                                    onClick = {
                                        accountFromOptionText = selectionOption
                                        accountFromExpanded = false
                                    }
                                )
                            }
                        }
                    }
                }

                // **** To Account ****
                if (showToAccountDropdown) {
                    var accountToExpanded by remember { mutableStateOf(false) }

                    ExposedDropdownMenuBox(
                        expanded = accountToExpanded,
                        onExpandedChange = { accountToExpanded = !accountToExpanded }
                    ) {
                        OutlinedTextField(
                            readOnly = true,
                            value = accountToOptionText,
                            onValueChange = {},
                            label = { Text("To Account") },
                            trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = accountToExpanded)},
                            modifier = Modifier
                                .menuAnchor(MenuAnchorType.PrimaryNotEditable, true)
                                .fillMaxWidth()
                        )

                        ExposedDropdownMenu(
                            expanded = accountToExpanded,
                            onDismissRequest = { accountToExpanded = false }
                        ) {
                            accountOptions.forEach { selectionOption ->
                                DropdownMenuItem(
                                    text = { Text(selectionOption) },
                                    onClick = {
                                        accountToOptionText = selectionOption
                                        accountToExpanded = false
                                    }
                                )
                            }
                        }
                    }
                }

                // **** Memo ****
                OutlinedTextField(
                    value = "",
                    onValueChange = {  },
                    label = { Text("Memo") },
                    trailingIcon = { Icon(Icons.AutoMirrored.Filled.KeyboardArrowRight, contentDescription = null) },
                    modifier = Modifier.fillMaxWidth()
                )
            }

            // Footer
            Column {
                SingleChoiceSegmentedButtonRow(
                    modifier = Modifier.fillMaxWidth()
                ) {
                    val statusOptions: List<String> = listOf("Pending", "Settled")
                    var selectedStatus by remember { mutableStateOf(0) }

                    statusOptions.forEachIndexed { index, label ->
                        SegmentedButton(
                            shape = SegmentedButtonDefaults.itemShape(
                                index = index,
                                count = statusOptions.size
                            ),
                            onClick = { selectedStatus = index },
                            selected = selectedStatus == index,
                            label = { Text(label) }
                        )
                    }
                }

                Button(
                    onClick = {
                        var isFormValid = true

                        // Amount Validation
                        val amountValue: Double?
                        if (textFieldValueState.text.isEmpty()) {
                            isFormValid = false
                            amountErrorType = CurrencyTextFieldError.Required
                        } else {
                            amountValue = textFieldValueState.text
                                .replace(',', '.')
                                .replace(" ", "")
                                .toDoubleOrNull()

                            if (amountValue == null) {
                                isFormValid = false
                                amountErrorType = CurrencyTextFieldError.InvalidFormat
                            }
                        }

                        if (isFormValid) {
                            // TODO: Send information to backend
                            // TODO: Wait for response
                            // TODO: Handle response

                            // If success, close transaction form
                            closingSheet = true
                        }
                    },
                    modifier = Modifier
                        .fillMaxWidth()
                ) {
                    Text("Add")
                }
            }
        }
    }
}