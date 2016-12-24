module OCRTest.Root

open System
open System.IO
open ImageMagick
open Tesseract

[<EntryPoint>]
let main argv = 
    use engine = new TesseractEngine("tessdata", "eng", EngineMode.Default)
    use img = Pix.LoadFromFile "test/test.tif"
    use page = engine.Process img

    let text = page.GetText()

    printfn "%s %f" text (page.GetMeanConfidence())
    
    Directory.CreateDirectory("output") |> ignore

    // Settings the density to 300 dpi will create an image with a better quality
    let settings = new MagickReadSettings(Density = new Density(300.))

    use images = new MagickImageCollection()

    // Add all the pages of the pdf file to the collection
    images.Read("test/Snakeware.pdf", settings)

    images 
    |> Seq.toList
    |> List.iteri (fun index image ->
        sprintf "output/Snakeware_%i.png" index
        |> image.Write
    )
    
    0 