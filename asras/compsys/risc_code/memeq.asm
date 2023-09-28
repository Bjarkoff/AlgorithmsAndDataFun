
	
.start:
	# Load address of data
	la a0, data1
	la a1, data2
#	lui a0, %hi(data1)
#	addi a0, a0, %lo(data1)
#	lui a1, %hi(data2)
#	addi a1, a1, %lo(data2)
	li a2, 6 # Length of array. Better way to set?
	
	call memeq
	
	# Now a0 = 1 if they are equal and a0 = 0 if they are not
	# Print a0
	call printchar
	
	
	
	j .end
	
	
memeq:
	# a0 contains address of first byte array
	# a1 contains address of second byte array
	# a2 contains length of arrays
	addi sp, sp, -4 # Reserve words for: ra, addr1, addr2
	sw ra, 0(sp)
	
	
	# Offset into array
	li t1, 0
	
.loop:
	# Check if we have reached the end of the array
	bge t1, a2, .equal
	
	# Load from first array
	lb t2, 0(a0)
	# Load from second array
	lb t3, 0(a1)
	
	# If not equal jump to notequal section which sets return value
	bne t2, t3, .notequal
	
	# Increment counter
	addi t1, t1, 1
	# Increment pointers
	addi a0, a0, 1
	addi a1, a1, 1

	j .loop
	

.equal:
	li a0, 1
	j .ret
	
.notequal:
	li a0, 0
	j .ret

.ret:
	
	lw ra, 0(sp)
	addi sp, sp, 4
	ret
	

printchar:
	addi sp, sp, -4
	sw ra, 0(sp)
	# a0 contains char
	li a7, 1
	ecall
	# Print newline
	li a0, 10
	li a7, 11
	ecall
	
	lw ra, 0(sp)
	addi sp, sp, 4
	ret	

.end:

	
	
.data
data1:
	.ascii "012345"
data2:
	.ascii "012345"
