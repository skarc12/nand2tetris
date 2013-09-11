// Runs an infinite loop that listens to the keyboard input. 
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, the
// program clears the screen, i.e. writes "white" in every pixel.

(LOOP)
	@KBD
	D=M
	@KEEP_WHITE
	D;JEQ
	D=-1 // black
	(KEEP_WHITE)

	@color   // color = D
	M=D

	@SCREEN
	D=A
	@idx  // idx = screen base address
	M=D

(COLOURING_LOOP)
	@idx
	D=M
	@KBD // keyboard starts just after screen
	D=D-A
	@LOOP
	D;JGE

	// colour single pixel:
	@color
	D=M
	@idx
	A=M
	M=D

	// move to the next pixel
	D=A+1
	@idx
	M=D

	// close the colouring loop
	@COLOURING_LOOP
	0;JMP