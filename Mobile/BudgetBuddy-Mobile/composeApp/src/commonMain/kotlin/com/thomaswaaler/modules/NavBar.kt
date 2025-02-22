package com.thomaswaaler.modules

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Home
import androidx.compose.material.icons.outlined.Home
import androidx.compose.material3.Icon
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.graphics.vector.ImageVector
import budgetbuddy.composeapp.generated.resources.Res
import budgetbuddy.composeapp.generated.resources.graph_2_24dp
import budgetbuddy.composeapp.generated.resources.graph_2_24px_500
import budgetbuddy.composeapp.generated.resources.sync_alt_24dp
import budgetbuddy.composeapp.generated.resources.tune_24dp
import org.jetbrains.compose.resources.vectorResource

enum class NavBarScreens
{
    Home,
    Transfer,
    Transactions,
    Config
}

@Composable
fun NavBar(
    selectedIndex: NavBarScreens,
    onSelectionChanged: (NavBarScreens) -> Unit
) {
    data class NavItem(val screenId: NavBarScreens, val label: String, val icon: ImageVector, val selectedIcon: ImageVector)
    val items = listOf(
        NavItem(NavBarScreens.Home, "Home", Icons.Outlined.Home, Icons.Filled.Home),
        NavItem(NavBarScreens.Transfer, "Transfer", vectorResource(Res.drawable.graph_2_24dp), vectorResource(Res.drawable.graph_2_24px_500)),
        NavItem(NavBarScreens.Transactions, "Transactions", vectorResource(Res.drawable.sync_alt_24dp), vectorResource(Res.drawable.sync_alt_24dp)),
        NavItem(NavBarScreens.Config, "Config", vectorResource(Res.drawable.tune_24dp), vectorResource(Res.drawable.tune_24dp))
    )

    NavigationBar {
        items.forEach { item ->
            val isSelected = selectedIndex == item.screenId
            val icon = if (isSelected) item.selectedIcon else item.icon

            NavigationBarItem(
                icon = { Icon(icon, contentDescription = item.label) },
                label = { Text(item.label) },
                selected = isSelected,
                onClick = { onSelectionChanged(item.screenId) }
            )
        }
    }
}