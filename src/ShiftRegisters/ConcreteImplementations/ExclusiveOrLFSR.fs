namespace ShiftRegisters.ConcreteImplementations

open ShiftRegisters

/// The most common LFSR feeds the XOR of predefined "taps"
/// back into the first register of the shift register.
/// The taps are identified using a 1-based index id, which 
/// ascends in the direction that the shift register shifts.
/// The higher the tap number, the lower the significance of
/// that bit.
/// Note: If the initial state is 0, then the register is 
/// initialized with a 1 in the left most (most significant)
/// bit.
type ExclusiveOrLFSR (initialState:bigint, taps:int list) =
    inherit LinearFeedbackShiftRegister (initialState, List.max taps)        

    /// Constructor that takes an integer as the initial value instead
    /// of a bigint. Also takes a list of taps.
    new(init:int, taps) = ExclusiveOrLFSR(bigint init, taps)

    /// Constructor that only takes the list of taps, and initializes
    /// the shift register with a 1 in the left most (most significant)
    /// bit.
    new(taps:int list) = ExclusiveOrLFSR((List.max >> pown 2) taps, taps)

    /// List of "taps", which are the Shift Register bits that
    /// are XORed together in order to produce the next input
    /// into the shift register.
    member __.Taps = taps

    /// Overridden feedback function. Takes the bit values at each
    /// of the predefined taps and XORs all of them together.
    override this.FeedbackFunction () =
        this.Taps
        |> List.map ((fun tap -> this.Length - tap) >> this.GetBit)
        |> List.reduce (<>)
