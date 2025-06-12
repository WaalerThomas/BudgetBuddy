package com.thomaswaaler.interfaces.requests

import com.thomaswaaler.models.Hearthbeat

interface HeartbeatApi {
    suspend fun get(): Hearthbeat
}