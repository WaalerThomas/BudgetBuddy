package com.thomaswaaler

// NOTE: Using https://www.youtube.com/watch?v=g4XSWQ7QT8g

import androidx.compose.material.MaterialTheme
import androidx.compose.runtime.*
import com.arkivanov.decompose.extensions.compose.stack.Children
import com.arkivanov.decompose.extensions.compose.stack.animation.slide
import com.arkivanov.decompose.extensions.compose.stack.animation.stackAnimation
import com.arkivanov.decompose.extensions.compose.subscribeAsState
import com.thomaswaaler.navigation.RootComponent
import com.thomaswaaler.screens.ScreenA
import com.thomaswaaler.screens.ScreenB

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

@Composable
fun App(root: RootComponent) {
    MaterialTheme {
        val childStack by root.childStack.subscribeAsState()
        Children(
            stack = childStack,
            animation = stackAnimation(slide())
        ) { child ->
            when (val instance = child.instance) {
                is RootComponent.Child.ScreenA -> ScreenA(instance.component)
                is RootComponent.Child.ScreenB -> ScreenB(instance.component)
            }
        }
    }
}

@Composable
fun MainApp()
{
    //val navController = rememberNavController()

    /*
    var showNavBar by remember { mutableStateOf(true) }
    var showTopBar by remember { mutableStateOf(true) }
    var showBackButton by remember { mutableStateOf(false) }
    var topBarTitle by remember { mutableStateOf("Budget Buddy") }

    var currentNavBarScreen: NavBarScreens by remember { mutableStateOf(NavBarScreens.Home) }

    MaterialTheme {
        Scaffold(
            topBar = {
                if (showTopBar) {
                    BudgetBAppBar(
                        title = topBarTitle,
                        canNavigateBack = showBackButton,
                        isSettings = false/*navController.currentDestination?.route != AppScreens.Settings.name*/,
                        onSettingsClicked = { /* navController.navigate(AppScreens.Settings.name) */ },
                        onBackClicked = { /* navController.popBackStack() */ }
                    )
                }
            },
            bottomBar = {
                if (showNavBar) {
                    NavBar(
                        onSelectionChanged = {
                            if (it == NavBarScreens.Home) {
                                /* navController.popBackStack(NavBarScreens.Home.name, false) */
                            } else {
                                /* navController.navigate(it.name) */
                            }
                        },
                        currentScreens = currentNavBarScreen
                    )
                }
            }
        ) { innerPadding ->
            Surface(
                modifier = Modifier
                    .fillMaxSize()
                    .padding(innerPadding)
            ) {
                HomeScreen()
            }
            /*NavHost(
                navController = navController,
                startDestination = AppScreens.Main.name,
                modifier = Modifier
                    .fillMaxSize()
                    .padding(innerPadding)
            )
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
            }*/
        }
    }
     */
}