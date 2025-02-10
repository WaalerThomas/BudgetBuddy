package com.thomaswaaler.budgetbuddy.android.modules

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Home
import androidx.compose.material3.Icon
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.graphics.vector.ImageVector
import com.thomaswaaler.budgetbuddy.android.NavBarScreens

@Composable
fun NavBar(
    currentScreens: NavBarScreens,
    onSelectionChanged: (NavBarScreens) -> Unit
) {
    data class NavItem(val screenId: NavBarScreens, val label: String, val icon: ImageVector)
    val items = listOf(
        NavItem(NavBarScreens.Home, "Home", Icons.Filled.Home),
        NavItem(NavBarScreens.Transfer, "Transfer", Icons.Filled.Home),
        NavItem(NavBarScreens.Transactions, "Transactions", Icons.Filled.Home),
        NavItem(NavBarScreens.Config, "Config", Icons.Filled.Home),
    )

    NavigationBar {
        items.forEach { item ->
            NavigationBarItem(
                icon = { Icon(item.icon, contentDescription = item.label) },
                label = { Text(item.label) },
                selected = currentScreens == item.screenId,
                onClick = {
                    onSelectionChanged(item.screenId)
                }
            )
        }
    }
}