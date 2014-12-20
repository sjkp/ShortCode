namespace SJKP.ShortCode

module ShortCode = 
    open System
    let r = new System.Random(Guid.NewGuid().GetHashCode())
    
    let urlSafeString (s : string) = 
        s.Replace('+','-').Replace('/','_').Replace("=","") 

    let private toBinary (x : int) = 
        System.Convert.ToString(x,2)
    
    let private fromBinary (x : string) = 
        System.Convert.ToInt32(x, 2)


    let private randomString(len : int) =        
        let a = Array.init len (fun i -> byte(i))
        r.NextBytes(a)
        a

    let private getDatePart(d : DateTime) = 
        let b = toBinary(Convert.ToInt32(d.ToString("yy"))).PadLeft(7,'0') + toBinary(d.Month).PadLeft(4,'0') + toBinary(d.Day).PadLeft(5,'0')
        let arr = [|Convert.ToByte(fromBinary(b.Substring(0,8))); Convert.ToByte(fromBinary(b.Substring(8)))|]
        (arr |> Convert.ToBase64String)

    let public NewShortCodeByDate(d : DateTime) = 
        getDatePart(d) + (randomString(5) |> Convert.ToBase64String) |> urlSafeString
    
    let public NewShortCode() = 
        let d = DateTime.UtcNow
        NewShortCodeByDate(d)
    
    let public GetDate(s: string) =
        let dpart = s.Replace('-','+').Replace('_','/').Substring(0,3) + "="
        let b = Convert.FromBase64String(dpart)
        let bstring = Convert.ToString(b.[0],2).PadLeft(8,'0') + Convert.ToString(b.[1],2).PadLeft(8,'0')
        let year = 2000+fromBinary(bstring.Substring(0,7))
        let month = fromBinary(bstring.Substring(7,4))
        let day = fromBinary(bstring.Substring(11))
        new DateTime(year,month,day)

    let public ConvertToDatePart(d : DateTime) = 
        getDatePart(d) |> urlSafeString