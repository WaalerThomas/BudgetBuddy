package com.thomaswaaler

import androidx.compose.foundation.layout.padding
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material3.FloatingActionButton
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import com.arkivanov.decompose.extensions.compose.stack.Children
import com.arkivanov.decompose.extensions.compose.stack.animation.stackAnimation
import com.arkivanov.decompose.extensions.compose.subscribeAsState
import com.thomaswaaler.modules.NavBar
import com.thomaswaaler.modules.BudgetBAppBar
import com.thomaswaaler.modules.NavBarScreens
import com.thomaswaaler.modules.RegisterBottomSheet
import com.thomaswaaler.navigation.RootComponent
import com.thomaswaaler.screens.ConfigScreen
import com.thomaswaaler.screens.HomeScreen
import com.thomaswaaler.screens.SettingsScreen
import com.thomaswaaler.screens.TransactionsScreen
import com.thomaswaaler.screens.TransferScreen

@Composable
fun App(root: RootComponent) {
    MaterialTheme {
        val childStack by root.childStack.subscribeAsState()

        var showBottomSheet by remember { mutableStateOf(false) }
        var showNavBar by remember { mutableStateOf(true) }
        var showTopBar by remember { mutableStateOf(true) }
        var showBackButton by remember { mutableStateOf(false) }

        var topBarTitle by remember { mutableStateOf("Budget Buddy") }

        var currentNavIndex by remember { mutableStateOf(NavBarScreens.Home) }

        fun setSettingsVariables() {
            showNavBar = false
            showTopBar = true
            showBackButton = true
        }

        fun setNavScreenVariables() {
            showNavBar = true
            showTopBar = true
            showBackButton = false
        }

        Scaffold(
            topBar = {
                if (showTopBar) {
                    BudgetBAppBar(
                        title = topBarTitle,
                        canNavigateBack = showBackButton,
                        isSettings = childStack.active.configuration == RootComponent.Configuration.SettingsScreen,
                        onSettingsClicked = { root.navigateToScreen(RootComponent.Configuration.SettingsScreen) },
                        onBackClicked = { root.navigateBack() }
                    )
                }
            },
            bottomBar = {
                if (showNavBar) {
                    NavBar(
                        selectedIndex = currentNavIndex,
                        onSelectionChanged = {
                            root.navigateNavBar(it)
                        }
                    )
                }
            },
            floatingActionButton = {
                FloatingActionButton(onClick = { showBottomSheet = true }) {
                    Icon(Icons.Filled.Add, contentDescription = "")
                }
            }
        ) { innerPadding ->
            Children(
                stack = childStack,
                animation = stackAnimation(),
                modifier = Modifier
                    .padding(innerPadding)
            ) { child ->
                when (val instance = child.instance) {
                    is RootComponent.Child.HomeScreen -> {
                        currentNavIndex = NavBarScreens.Home
                        topBarTitle = "Budget Buddy"
                        setNavScreenVariables()

                        HomeScreen(instance.component)
                    }

                    is RootComponent.Child.SettingsScreen -> {
                        topBarTitle = "Settings"
                        setSettingsVariables()

                        SettingsScreen(instance.component)
                    }

                    is RootComponent.Child.ConfigScreen -> {
                        currentNavIndex = NavBarScreens.Config
                        topBarTitle = "Config"
                        setNavScreenVariables()

                        ConfigScreen(instance.component)
                    }

                    is RootComponent.Child.TransactionsScreen -> {
                        currentNavIndex = NavBarScreens.Transactions
                        topBarTitle = "Transactions"
                        setNavScreenVariables()

                        TransactionsScreen(instance.component)
                    }

                    is RootComponent.Child.TransferScreen -> {
                        currentNavIndex = NavBarScreens.Transfer
                        topBarTitle = "Transfer"
                        setNavScreenVariables()

                        TransferScreen(instance.component)
                    }
                }

                if (showBottomSheet) {
                    RegisterBottomSheet(onDismiss = { showBottomSheet = false })
                }
            }
        }
    }
}