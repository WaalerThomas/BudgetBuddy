package com.thomaswaaler.screens

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.thomaswaaler.navigation.TransactionsScreenComponent

@Composable
fun TransactionsScreen(
    component: TransactionsScreenComponent
) {
    Column(
        modifier = Modifier.padding(8.dp)
    ) {
        Text("Hello from transactions screen")
        Button(
            modifier = Modifier.fillMaxWidth(),
            onClick = { }
        ) {
            Text("Reconcile")
        }
    }
}