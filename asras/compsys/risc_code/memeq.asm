.globl memeq
memeq:
	# a0 contains address of first byte array
	# a1 contains address of second byte array
	# a2 contains length of arrays
	
	# Offset into array
	li t1, 0
memeq.loop:
	# Check if we have reached the end of the array
	bge t1, a2, memeq.equal
	
	# Load from first array
	lb t2, 0(a0)
	# Load from second array
	lb t3, 0(a1)
	
	# If not equal jump to notequal section which sets return value
	bne t2, t3, memeq.notequal
	
	# Increment counter
	addi t1, t1, 1
	# Increment pointers
	addi a0, a0, 1
	addi a1, a1, 1

	j memeq.loop
	

memeq.equal:
	li a0, 1
	j memeq.ret
	
memeq.notequal:
	li a0, 0
	j memeq.ret

memeq.ret:
	ret
	
