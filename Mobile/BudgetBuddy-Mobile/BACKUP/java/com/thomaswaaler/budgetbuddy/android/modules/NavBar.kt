package com.thomaswaaler.budgetbuddy.android.modules

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Home
import androidx.compose.material3.Icon
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.graphics.vector.ImageVector
import androidx.compose.ui.res.vectorResource
import androidx.compose.ui.tooling.preview.Preview
import com.thomaswaaler.budgetbuddy.android.NavBarScreens
import com.thomaswaaler.budgetbuddy.android.R

@Composable
fun NavBar(
    currentScreens: NavBarScreens,
    onSelectionChanged: (NavBarScreens) -> Unit
) {
    data class NavItem(val screenId: NavBarScreens, val label: String, val icon: ImageVector)
    val items = listOf(
        NavItem(NavBarScreens.Home, "Home", Icons.Filled.Home),
        NavItem(NavBarScreens.Transfer, "Transfer", ImageVector.vectorResource(id = R.drawable.graph_2_24dp)),
        NavItem(NavBarScreens.Transactions, "Transactions", ImageVector.vectorResource(id = R.drawable.sync_alt_24dp)),
        NavItem(NavBarScreens.Config, "Config", ImageVector.vectorResource(id = R.drawable.tune_24dp)),
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

@Preview
@Composable
private fun PreviewNavBar()
{
    NavBar(currentScreens = NavBarScreens.Home) {}
}