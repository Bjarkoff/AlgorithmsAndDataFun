.globl printchar
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

.globl exit0
exit0:
    # Exit syscall
    li a0, 0
    li a7, 93
    ecall
