﻿module Common

open System
open FSharp.Data

type ProductDb = XmlProvider<"./CommerceDb.xml">
let productsFromDatabase = ProductDb.GetSample()

[<Literal>]
let BlackColor = "Black"

[<Literal>] 
let WhiteColor = "White"

/// Weight in kilograms.
[<Measure>] type kg

let myWeight = 5<kg>

/// Pricing in USD currency.
[<Measure>] type usd

/// Amount of stars received from a review.
[<Measure>] type star

/// Smallest addressable element in an all points addressable display device
[<Measure>] type pixel

/// Unit of length in the metric system, equal to one hundredth of a metre
[<Measure>] type cm

/// Megabyte (MB), a measure of information. Megabit (Mb or Mbit), a measure of information. MikroBitti (formerly MB), a Finnish computer magazine. Mega base pairs, a unit of measurement in genetics.
[<Measure>] type Mb

/// The gigabyte is a multiple of the unit byte for digital information. The prefix giga means 10^9 in the International System of Units (SI). 
[<Measure>] type Gb

/// A central processing unit (CPU), also called a central processor or main processor, is the electronic circuitry within a computer that carries out the instructions of a computer program by performing the basic arithmetic, logic, controlling, and input/output (I/O) operations specified by the instructions. 
[<Measure>] type Ghz

/// The watt (symbol: W) is a unit of power. In the International System of Units (SI) it is defined as a derived unit of 1 joule per second and is used to quantify the rate of energy transfer
[<Measure>] type watt

/// An hour (abbreviated hr.) is a unit of time conventionally reckoned as ​1⁄24 of a day and scientifically reckoned as 3,599–3,601 seconds, depending on conditions. 
[<Measure>] type hr

let (|Prefix|_|) (p:string) (s:string) =
    if s.StartsWith(p) then
        Some(s.Substring(p.Length))
    else
        None

let (|Suffix|_|) (p:string) (s:string) =
    if s.EndsWith(p) then
        Some(String.Empty)
    else
        None

let inline castToUsd value = value * 1.00m<usd>

let inline castToKg (value: decimal) = (value |> float) * 1.00<kg>

let inline castToStarReview (value: decimal) = (value |> float) * 1.00<star>

let inline castToHour value = (value |> sbyte) * 1y<hr>

let inline castToCm value = (value |> decimal) * 1.00m<cm>

type ProductDimension = {
    Heigth: decimal<cm>; 
    Width: decimal<cm>; 
    Depth: decimal<cm> option;
}

type ProductColor = 
    | Red
    | Black
    | White
    | Gray
    | Blue
    | Green 
    | NotSupportedByStore

type Brand = 
    | Toshiba
    | Sony
    | Microsoft
    | Intel
    | AMD
    | Nintendo
    | Bose  
    | Asus
    | Apple
    | NotSupportedByStore

type SupportedLanguage = 
    | English
    | French
    | NotSupportedByStore

type GeneratedTypeFromStore = 
    | Headphones                of value: ProductDb.Headphone
    | ReadingMaterial           of value: ProductDb.Book
    | Computer                  of value: ProductDb.Computer
    //| Television    of value: ProductDb.Television
    //| GameConsole   of value: ProductDb.GameConsole

type CommonProductInformation  = { 
    Name:           string 
    Weight:         float<kg>
    ShippingWeight: float<kg>
    AverageReviews: float<star>
    Dimensions:     ProductDimension
    Price:          decimal<usd>
    Color:          ProductColor
    Brand:          Brand
}