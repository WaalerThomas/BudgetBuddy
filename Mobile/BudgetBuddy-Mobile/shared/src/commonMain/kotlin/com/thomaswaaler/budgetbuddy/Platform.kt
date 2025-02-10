package com.thomaswaaler.budgetbuddy

interface Platform {
    val name: String
}

expect fun getPlatform(): Platform