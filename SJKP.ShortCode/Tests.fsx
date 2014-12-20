// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "ShortCode.fs"
open SJKP.ShortCode
open System


let d = new DateTime(2001,2,28)
let dpart = ShortCode.ConvertToDatePart(d)
let res = ShortCode.NewShortCodeByDate(d)
let res3 = ShortCode.NewShortCodeByDate(d)
let res1 = ShortCode.GetDate(res)
