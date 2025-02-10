package com.thomaswaaler.budgetbuddy.android.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.unit.dp
import com.thomaswaaler.budgetbuddy.android.R

@Composable
fun HomeScreen()
{
    Surface(
        modifier = Modifier
            .fillMaxSize(),
        color = MaterialTheme.colorScheme.tertiaryContainer
    ) {
        Column(
            modifier = Modifier.fillMaxSize(),
            verticalArrangement = Arrangement.spacedBy(16.dp),
        ) {
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceEvenly
            ) {
                Icon(painter = painterResource(id = R.drawable.circle_24dp_filled), contentDescription = null, tint = Color.Green)
                Icon(painter = painterResource(id = R.drawable.clock_loader_80_24dp), contentDescription = null, tint = Color.Green)
                Icon(painter = painterResource(id = R.drawable.clock_loader_60_24dp), contentDescription = null, tint = Color.hsl(63F, .93F, .70F))
                Icon(painter = painterResource(id = R.drawable.clock_loader_40_24dp), contentDescription = null, tint = Color.hsl(63F, 1F, .50F))
                Icon(painter = painterResource(id = R.drawable.clock_loader_10_24dp), contentDescription = null, tint = Color.hsl(37F, .87F, .54F))
                Icon(painter = painterResource(id = R.drawable.circle_24dp_outlined), contentDescription = null, tint = Color.Red)
            }

            Icon(painter = painterResource(id = R.drawable.currency_exchange_24dp), contentDescription = null)
            Icon(painter = painterResource(id = R.drawable.list_alt_24dp), contentDescription = null)

            Row {
                Icon(painter = painterResource(id = R.drawable.trending_down_24dp), contentDescription = null, tint = Color.Red)
                Icon(painter = painterResource(id = R.drawable.trending_flat_24dp), contentDescription = null)
                Icon(painter = painterResource(id = R.drawable.trending_up_24dp), contentDescription = null, tint = Color.Green)
            }
        }
    }
}