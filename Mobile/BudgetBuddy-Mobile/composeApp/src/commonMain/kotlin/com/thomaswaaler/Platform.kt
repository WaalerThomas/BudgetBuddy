package com.thomaswaaler

interface Platform {
    val name: String
}

expect fun getPlatform(): Platform