// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[3], respectively.)

// Put your code here.

	@R0
	D=M
	@a
	M=D  // a = R0

	@R1
	D=M
	@b
	M=D // b = R1

	@0
	D=A
	@R2
	M=D // R2 =0

(WHILE)
	@a
	D=M
	@END
	D;JLE	// if a<=0 goto END

	@b
	D=M  	// D=b
	@R2
	M=D+M 	// R2 += b

	// @1
	// D=A 	// D = 1
	@a
	M=M-1	// a = a - 1

	@WHILE
	0;JMP

(END)
	@END
	0;JMP // infinite loop

