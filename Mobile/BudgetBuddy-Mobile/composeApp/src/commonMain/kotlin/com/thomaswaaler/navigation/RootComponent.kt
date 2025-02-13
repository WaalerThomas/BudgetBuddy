package com.thomaswaaler.navigation

import com.arkivanov.decompose.ComponentContext
import com.arkivanov.decompose.router.stack.StackNavigation
import com.arkivanov.decompose.router.stack.childStack
import com.arkivanov.decompose.router.stack.pop
import com.arkivanov.decompose.router.stack.popToFirst
import com.arkivanov.decompose.router.stack.pushToFront
import com.thomaswaaler.modules.NavBarScreens
import kotlinx.serialization.Serializable

class RootComponent(
    componentContext: ComponentContext
): ComponentContext by componentContext {

    private val navigation = StackNavigation<Configuration>()
    val childStack = childStack(
        source = navigation,
        serializer = Configuration.serializer(),
        initialConfiguration = Configuration.HomeScreen,
        handleBackButton = true,
        childFactory = ::createChild
    )

    private fun createChild(
        config: Configuration,
        context: ComponentContext
    ): Child {
        return when(config) {
            /*
            is Configuration.ScreenA -> Child.ScreenA(ScreenAComponent(
                componentContext = context,
                onNavigateToScreenB = { text ->
                    navigation.pushNew(Configuration.ScreenB(text))
                }
            ))

            is Configuration.ScreenB -> Child.ScreenB(ScreenBComponent(
                text = config.text,
                componentContext = context,
                onGoBack = { navigation.pop() }
            ))
            */

            is Configuration.HomeScreen -> Child.HomeScreen(HomeScreenComponent(
                componentContext = context
            ))

            is Configuration.SettingsScreen -> Child.SettingsScreen(SettingsScreenComponent(
                componentContext = context
            ))

            is Configuration.ConfigScreen -> Child.ConfigScreen(ConfigScreenComponent(
                componentContext = context
            ))

            is Configuration.TransactionsScreen -> Child.TransactionsScreen(
                TransactionsScreenComponent(
                componentContext = context
            )
            )

            is Configuration.TransferScreen -> Child.TransferScreen(
                TransferScreenComponent(
                componentContext = context
            )
            )
        }
    }

    fun navigateNavBar(index: NavBarScreens) {
        if (index == NavBarScreens.Home)
        {
            navigation.popToFirst()
        }
        else
        {
            when(index) {
                NavBarScreens.Home -> navigation.pushToFront(Configuration.HomeScreen)
                NavBarScreens.Transfer -> navigation.pushToFront(Configuration.TransferScreen)
                NavBarScreens.Transactions -> navigation.pushToFront(Configuration.TransactionsScreen)
                NavBarScreens.Config -> navigation.pushToFront(Configuration.ConfigScreen)
            }
        }
    }

    fun navigateToScreen(config: Configuration) {
        navigation.pushToFront(config)
    }

    fun navigateBack() {
        navigation.pop()
    }

    sealed class Child {
        data class HomeScreen(val component: HomeScreenComponent): Child()
        data class TransferScreen(val component: TransferScreenComponent): Child()
        data class TransactionsScreen(val component: TransactionsScreenComponent): Child()
        data class ConfigScreen(val component: ConfigScreenComponent): Child()
        data class SettingsScreen(val component: SettingsScreenComponent): Child()
    }

    @Serializable
    sealed class Configuration {
        @Serializable
        data object HomeScreen: Configuration()

        @Serializable
        data object TransferScreen: Configuration()

        @Serializable
        data object TransactionsScreen: Configuration()

        @Serializable
        data object ConfigScreen: Configuration()

        @Serializable
        data object SettingsScreen: Configuration()

        //@Serializable
        //data class ScreenB(val text: String): Configuration()
    }
}