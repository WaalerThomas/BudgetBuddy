package com.thomaswaaler.utils

expect class DateUtil() {

    fun getCurrentDate(): String
    fun millisecondsToDate(): String
    fun dateToMilliseconds(): Long
}
