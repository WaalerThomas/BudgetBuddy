package com.thomaswaaler.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import budgetbuddy.composeapp.generated.resources.Res
import budgetbuddy.composeapp.generated.resources.circle_24dp_filled
import budgetbuddy.composeapp.generated.resources.circle_24dp_outlined
import budgetbuddy.composeapp.generated.resources.clock_loader_10_24dp
import budgetbuddy.composeapp.generated.resources.clock_loader_40_24dp
import budgetbuddy.composeapp.generated.resources.clock_loader_60_24dp
import budgetbuddy.composeapp.generated.resources.clock_loader_80_24dp
import budgetbuddy.composeapp.generated.resources.currency_exchange_24dp
import budgetbuddy.composeapp.generated.resources.error_24dp
import budgetbuddy.composeapp.generated.resources.graph_2_24dp
import budgetbuddy.composeapp.generated.resources.list_alt_24dp
import budgetbuddy.composeapp.generated.resources.sync_alt_24dp
import budgetbuddy.composeapp.generated.resources.trending_down_24dp
import budgetbuddy.composeapp.generated.resources.trending_flat_24dp
import budgetbuddy.composeapp.generated.resources.trending_up_24dp
import com.thomaswaaler.navigation.HomeScreenComponent
import org.jetbrains.compose.resources.painterResource
import org.jetbrains.compose.resources.vectorResource

@Composable
fun HomeScreen(
    component: HomeScreenComponent
) {
    Surface(
        modifier = Modifier
            .fillMaxSize()
            .padding(8.dp),
        color = MaterialTheme.colorScheme.surface,
        contentColor = MaterialTheme.colorScheme.onSurface
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize(),
            verticalArrangement = Arrangement.spacedBy(16.dp)
        ) {
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceEvenly
            ) {
                Icon(vectorResource(Res.drawable.circle_24dp_filled), contentDescription = null, tint = Color.hsl(114F, .71F, .41F))
                Icon(vectorResource(Res.drawable.clock_loader_80_24dp), contentDescription = null, tint = Color.hsl(114F, .87F, .54F))
                Icon(vectorResource(Res.drawable.clock_loader_60_24dp), contentDescription = null, tint = Color.hsl(58F, .71F, .46F))
                Icon(vectorResource(Res.drawable.clock_loader_40_24dp), contentDescription = null, tint = Color.hsl(63F, .87F, .54F))
                Icon(vectorResource(Res.drawable.clock_loader_10_24dp), contentDescription = null, tint = Color.hsl(37F, .87F, .54F))
                Icon(vectorResource(Res.drawable.circle_24dp_outlined), contentDescription = null, tint = MaterialTheme.colorScheme.error)
                Icon(vectorResource(Res.drawable.error_24dp), contentDescription = null, tint = MaterialTheme.colorScheme.error)
            }

            Icon(vectorResource(Res.drawable.currency_exchange_24dp), contentDescription = null)
            Icon(vectorResource(Res.drawable.list_alt_24dp), contentDescription = null)

            Row {
                Icon(vectorResource(Res.drawable.trending_down_24dp), contentDescription = null, tint = Color.Red)
                Icon(vectorResource(Res.drawable.trending_flat_24dp), contentDescription = null)
                Icon(vectorResource(Res.drawable.trending_up_24dp), contentDescription = null, tint = Color.Green)
            }
        }
    }
}