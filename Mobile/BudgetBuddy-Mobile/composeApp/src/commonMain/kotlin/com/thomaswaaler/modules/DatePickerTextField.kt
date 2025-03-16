package com.thomaswaaler.modules

import androidx.compose.foundation.gestures.awaitEachGesture
import androidx.compose.foundation.gestures.awaitFirstDown
import androidx.compose.foundation.gestures.waitForUpOrCancellation
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.DateRange
import androidx.compose.material3.DatePicker
import androidx.compose.material3.DatePickerDialog
import androidx.compose.material3.DisplayMode
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.material3.rememberDatePickerState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.pointer.PointerEventPass
import androidx.compose.ui.input.pointer.pointerInput
import kotlinx.datetime.Clock
import kotlinx.datetime.Instant
import kotlinx.datetime.LocalDate
import kotlinx.datetime.TimeZone
import kotlinx.datetime.format
import kotlinx.datetime.format.MonthNames
import kotlinx.datetime.format.char
import kotlinx.datetime.toLocalDateTime

private val dateFormat = LocalDate.Format {
    dayOfMonth()
    char(' ')
    monthName(MonthNames.ENGLISH_FULL)
    chars(", ")
    year()
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun DatePickerTextField(
    value: Long,
    onValueChanged: (Long) -> Unit,
    modifier: Modifier
) {
    var showDatePicker by remember { mutableStateOf(false) }
    val datePickerState  = rememberDatePickerState(
        initialDisplayMode = DisplayMode.Picker,
        initialSelectedDateMillis = Clock.System.now().toEpochMilliseconds()
    )

    OutlinedTextField(
        value = Instant.fromEpochMilliseconds(value).toLocalDateTime(TimeZone.currentSystemDefault()).date.format(dateFormat),
        onValueChange = { },
        readOnly = true,
        trailingIcon = { Icon(Icons.Default.DateRange, contentDescription = "Select date") },
        modifier = modifier
            .pointerInput(datePickerState.selectedDateMillis) {
                awaitEachGesture {
                    awaitFirstDown(pass = PointerEventPass.Initial)
                    val upEvent = waitForUpOrCancellation(pass = PointerEventPass.Initial)
                    if (upEvent != null) {
                        datePickerState.selectedDateMillis = value
                        showDatePicker = true
                    }
                }
            }
    )

    if (showDatePicker) {
        DatePickerDialog(
            onDismissRequest = {},
            confirmButton = {
                TextButton(onClick = {
                    onValueChanged(datePickerState.selectedDateMillis!!)
                    showDatePicker = false
                }) {
                    Text("OK")
                }
            },
            dismissButton = {
                TextButton(onClick = { showDatePicker = false }) {
                    Text("Cancel")
                }
            }
        ) {
            DatePicker(
                state = datePickerState,
                showModeToggle = false
            )
        }
    }
}