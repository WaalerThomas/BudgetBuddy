package com.thomaswaaler.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import com.thomaswaaler.navigation.ScreenBComponent

@Composable
fun ScreenB(component: ScreenBComponent) {
    Column(
        modifier = Modifier
            .fillMaxSize(),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Text("Screen B: ${component.text}")
        Button(onClick = {
            component.goBack()
        }) {
            Text("Go back")
        }
    }
}