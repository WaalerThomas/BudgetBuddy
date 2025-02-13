package com.thomaswaaler.screens

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.thomaswaaler.navigation.TransferScreenComponent

@Composable
fun TransferScreen(
    component: TransferScreenComponent
) {
    Surface(
        modifier = Modifier
            .fillMaxSize()
            .padding(5.dp)
    ) {
        Column {
            Text("Hello from transfer screen")
            Button(onClick = {}) {
                Text("Nothing Button")
            }
        }
    }
}