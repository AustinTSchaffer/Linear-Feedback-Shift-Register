namespace ShiftRegisters

/// The most common LFSR feeds the XOR of predefined "taps"
/// back into the first register of the shift register.
type ExclusiveOrLFSR (initialState:bigint, taps:int list) =
    inherit LinearFeedbackShiftRegister (initialState, List.max taps)

    member __.Taps = taps

    override this.FeedbackFunction () =
        this.Taps
        |> List.map ((fun tap -> this.Length - tap) >> this.GetBit)
        |> List.reduce (<>)
        