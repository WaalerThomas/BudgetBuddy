package com.thomaswaaler.modules

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Home
import androidx.compose.material.icons.outlined.AccountCircle
import androidx.compose.material.icons.outlined.Home
import androidx.compose.material3.Icon
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.graphics.vector.ImageVector

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
        NavItem(NavBarScreens.Transfer, "Transfer", Icons.Outlined.AccountCircle, Icons.Filled.AccountCircle),
        NavItem(NavBarScreens.Transactions, "Transactions", Icons.Outlined.AccountCircle, Icons.Filled.AccountCircle),
        NavItem(NavBarScreens.Config, "Config", Icons.Outlined.AccountCircle, Icons.Filled.AccountCircle)
        //NavItem(1, "Transfer", ImageVector.vectorResource(id = R.drawable.graph_2_24dp)),
        //NavItem(2, "Transactions", ImageVector.vectorResource(id = R.drawable.sync_alt_24dp)),
        //NavItem(3, "Config", ImageVector.vectorResource(id = R.drawable.tune_24dp)),
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