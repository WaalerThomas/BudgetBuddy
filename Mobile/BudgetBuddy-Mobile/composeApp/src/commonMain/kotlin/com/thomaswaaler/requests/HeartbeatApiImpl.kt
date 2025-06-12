package com.thomaswaaler.requests

import com.thomaswaaler.interfaces.requests.HeartbeatApi
import com.thomaswaaler.models.Hearthbeat
import io.ktor.client.HttpClient
import io.ktor.client.call.body
import io.ktor.client.request.get
import io.ktor.http.URLProtocol
import io.ktor.http.path

// Follow this thing: https://mubaraknative.medium.com/making-an-http-request-using-ktor-client-a87c2593ed25

class HeartbeatApiImpl(
    private val client: HttpClient
) : HeartbeatApi {
    override suspend fun get(): Hearthbeat {
        return client.get {
            url {
                protocol = URLProtocol.HTTPS
                host = "localhost"
                path("api/heartbeat")
            }
        }.body()
    }
}