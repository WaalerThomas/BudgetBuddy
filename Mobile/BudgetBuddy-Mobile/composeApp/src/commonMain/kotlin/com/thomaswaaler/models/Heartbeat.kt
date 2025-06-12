package com.thomaswaaler.models

import kotlinx.serialization.Serializable

@Serializable
data class Hearthbeat(
    val status: Status,
    val data: Int,
    val correlationId: String?
)

@Serializable
data class Status(
    val code: Int,
    val message: String
)
