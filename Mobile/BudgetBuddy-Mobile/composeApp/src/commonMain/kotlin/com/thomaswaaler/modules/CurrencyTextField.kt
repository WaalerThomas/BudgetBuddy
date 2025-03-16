package com.thomaswaaler.modules

import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Info
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.TextRange
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.input.TextFieldValue
import androidx.compose.ui.text.input.getTextAfterSelection

enum class CurrencyTextFieldError {
    Required,
    InvalidFormat
}

private val errorMessages = mapOf(
    CurrencyTextFieldError.Required to "Required",
    CurrencyTextFieldError.InvalidFormat to "Invalid format"
)

@Composable
fun CurrencyTextField(
    value: TextFieldValue,
    onValueChange: (TextFieldValue) -> Unit,
    currencySuffix: String = "NOK",
    placeholder: String = "0,0",
    errorType: CurrencyTextFieldError? = null,
    onErrorTypeChanged: (CurrencyTextFieldError?) -> Unit,
    modifier: Modifier
) {
    val trailingIcon: @Composable (() -> Unit)? = if (errorType != null) {
        { Icon(Icons.Default.Info, contentDescription = null, tint = MaterialTheme.colorScheme.error) }
    } else {
        null
    }

    val supportingText: @Composable (() -> Unit)? = if (errorType != null) {
        { errorMessages[errorType]?.let { Text(it, color = MaterialTheme.colorScheme.error) } }
    } else {
        null
    }

    OutlinedTextField(
        value = value,
        onValueChange = {
            if (errorType != null) {
                onErrorTypeChanged(null)
            }

            onValueChange(formatAmountText(value, it))
        },
        keyboardOptions = KeyboardOptions.Default.copy(
            keyboardType = KeyboardType.Number
        ),
        singleLine = true,
        isError = errorType != null,
        suffix = { Text(currencySuffix) },
        placeholder = { Text(placeholder) },
        modifier = modifier,
        trailingIcon = trailingIcon,
        supportingText = supportingText
    )
}

private fun formatAmountText(textFieldValue: TextFieldValue, newTextFieldValue: TextFieldValue): TextFieldValue {
    // TODO (Problem): if zero is first number, then only allow one

    if (textFieldValue.text == newTextFieldValue.text) {
        return newTextFieldValue
    }

    val decimalPointCount = newTextFieldValue.text.count { x -> x == ',' || x == '.' }
    if (decimalPointCount > 1) {
        return textFieldValue
    }

    val originalWhitespaceCount = textFieldValue.text.count { x -> x == ' ' }
    val newWhitespaceCount = newTextFieldValue.text.count { x -> x == ' ' }
    var myString = if (originalWhitespaceCount != newWhitespaceCount)
        newTextFieldValue.text.removeRange(newTextFieldValue.selection.min - 1, newTextFieldValue.selection.max) else
        newTextFieldValue.text

    if (!isFirstCharacterValid(myString)) {
        return textFieldValue
    }

    if (!isLastCharacterValid(newTextFieldValue.text)) {
        return newTextFieldValue
    }

    // Figure out where the cursor is positioned
    val afterCursor = newTextFieldValue.getTextAfterSelection(newTextFieldValue.text.length)
    val textCount = afterCursor.length

    // Split into integer and decimal part
    myString = myString.replace(',', '.')
    myString = myString.replace(" ", "")
    val strings = myString.split(".")
    var integerPart = strings[0]
    var decimalPart: String? = null
    if (strings.size >= 2) {
        decimalPart = strings[1]
    }

    // Only allow two decimal numbers
    if (decimalPart != null && decimalPart.length > 2) {
        return textFieldValue
    }

    // Reverse the integer part to process from least significant digit
    integerPart = integerPart.reversed()

    // Insert a space every three digits
    var formattedIntegerPart = ""
    for (index in integerPart.indices) {
        if (index > 0 && index % 3 == 0) {
            formattedIntegerPart += " "
        }
        formattedIntegerPart += integerPart[index]
    }

    // Reverse back the formatted integer part
    formattedIntegerPart = formattedIntegerPart.reversed()

    // Combine the formatted integer part and the decimal part with a comma
    var formattedString = formattedIntegerPart
    if (decimalPart != null) {
        formattedString = formattedString.plus(",$decimalPart")
    }

    return TextFieldValue(
        text = formattedString,
        selection = TextRange(formattedString.length - textCount)
    )
}

private fun isFirstCharacterValid(text: String): Boolean {
    if (text.isEmpty()) {
        return true
    }

    val firstChar = text[0]

    return !(firstChar == '-' || firstChar == '+' || firstChar == ',' || firstChar == '.' || firstChar == ' ')
}

private fun isLastCharacterValid(text: String): Boolean {
    if (text.isEmpty()) {
        return true
    }

    val lastChar = text[text.lastIndex]

    return !(lastChar == ',' || lastChar == '.')
}