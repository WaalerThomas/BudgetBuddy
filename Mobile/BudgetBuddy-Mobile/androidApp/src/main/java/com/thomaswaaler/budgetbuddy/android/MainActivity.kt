package com.thomaswaaler.budgetbuddy.android

import android.os.Bundle
import android.util.Log
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.navigation
import androidx.navigation.compose.rememberNavController
import com.thomaswaaler.budgetbuddy.android.modules.BudgetBAppBar
import com.thomaswaaler.budgetbuddy.android.modules.NavBar
import com.thomaswaaler.budgetbuddy.android.screens.HomeScreen

enum class NavBarScreens {
    Home,
    Transfer,
    Transactions,
    Config,
}

enum class AppScreens() {
    Main,
    Settings
}

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            MyApplicationTheme {
                MainApp()
            }
        }
    }
}

@Composable
fun MainApp()
{
    val navController = rememberNavController()

    var showNavBar by remember { mutableStateOf(true) }
    var showTopBar by remember { mutableStateOf(true) }
    var showBackButton by remember { mutableStateOf(false) }
    var topBarTitle by remember { mutableStateOf("Budget Buddy") }

    var currentNavBarScreen: NavBarScreens by remember { mutableStateOf(NavBarScreens.Home) }

    Scaffold(
        topBar = {
            if (showTopBar) {
                BudgetBAppBar(
                    title = topBarTitle,
                    canNavigateBack = showBackButton,
                    isSettings = navController.currentDestination?.route != AppScreens.Settings.name,
                    onSettingsClicked = { navController.navigate(AppScreens.Settings.name) },
                    onBackClicked = { navController.popBackStack() }
                )
            }
        },
        bottomBar = {
            if (showNavBar) {
                NavBar(
                    onSelectionChanged = {
                        if (it == NavBarScreens.Home) {
                            navController.popBackStack(NavBarScreens.Home.name, false)
                        } else {
                            navController.navigate(it.name)
                        }
                    },
                    currentScreens = currentNavBarScreen
                )
            }
        }
    ) {innerPadding ->
        NavHost(
            navController = navController,
            startDestination = AppScreens.Main.name,
            modifier = Modifier
                .fillMaxSize()
                .padding(innerPadding))
        {
            composable(route = AppScreens.Settings.name) {
                showNavBar = false
                showTopBar = true
                showBackButton = true
                topBarTitle = "Settings"

                // SettingsScreen()
                Text(text = "Nothing here, settings empty")
            }

            navigation(
                startDestination = NavBarScreens.Home.name,
                route = AppScreens.Main.name
            ) {
                composable(route = NavBarScreens.Home.name) {
                    currentNavBarScreen = NavBarScreens.Home
                    topBarTitle = "Dashboard"

                    showNavBar = true
                    showTopBar = true
                    showBackButton = false

                    HomeScreen()
                }

                composable(route = NavBarScreens.Transfer.name) {
                    currentNavBarScreen = NavBarScreens.Transfer
                    topBarTitle = "Transfer"

                    showNavBar = true
                    showTopBar = true
                    showBackButton = false

                    Text(text = "Transfer page")
                }

                composable(route = NavBarScreens.Transactions.name) {
                    currentNavBarScreen = NavBarScreens.Transactions
                    topBarTitle = "Transactions"

                    showNavBar = true
                    showTopBar = true
                    showBackButton = false

                    Text(text = "Transactions page")
                }

                composable(route = NavBarScreens.Config.name) {
                    currentNavBarScreen = NavBarScreens.Config
                    topBarTitle = "Config"

                    showNavBar = true
                    showTopBar = true
                    showBackButton = false

                    Text(text = "Config page")
                }
            }
        }
    }
}