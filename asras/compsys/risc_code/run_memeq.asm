j main



.data
data1:
	.ascii "012345"
data2:
	.ascii "012345"


.text
.include "utils.asm"
.include "memeq.asm"	

main:
	# Load address of data
	la a0, data1
	la a1, data2
	li a2, 6 # Length of arrays. Better way to set?
	
	call memeq
	
	# Now a0 = 1 if they are equal and a0 = 0 if they are not
	# Print a0
	call printchar
	
	
    call exit0
	
