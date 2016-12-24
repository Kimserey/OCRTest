module OCRTest.Root

open ImageMagick
open System
open Tesseract

[<EntryPoint>]
let main argv = 
    use engine = new TesseractEngine("tessdata", "eng", EngineMode.Default)
    use img = Pix.LoadFromFile "test/test_2.png"
    use page = engine.Process img

    let text = page.GetText()

    printfn "%s %f" text (page.GetMeanConfidence())

    ImageMagick.PdfReadDefines

    Console.ReadKey() |> ignore
    0 