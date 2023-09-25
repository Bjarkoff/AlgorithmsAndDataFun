# RARS starts executing from the top :X
.start:
	li a0, 3
	call fib
	jal .end

fib:
	# Store the input value on the stack
	addi sp, sp, -12 # Reserve words for: Initial value of n, return of fib(n-1), return address
	sw a0, 0(sp)     # Store initial value of n
	sw ra, 8(sp)     # store return address
	
	# Input argument is in a0
	# We want to compare with the number 2 so we have to load that into a register (There is no blt immediate version)
	li t0, 2
	blt a0, t0, .base_case

	
	# Call fib recursively with n - 1
	addi a0, a0, -1
	call fib # Call puts something into ra, that's why we have to store it
	
	# Now return value is in a0 so store that on the stack
	sw a0, 4(sp)
	
	# Put n - 2 into a0
	lw a0, 0(sp)
	addi a0, a0, -2
	
	# Call fib
	call fib
	
	# Return value is again in a0, load value of fib(n-1) and add to a0
	lw t0, 4(sp)   # fib(n-1)
	add a0, a0, t0 # fib(n-1) + fib(n-2)
	
	jal .return
	
.base_case:
	li a0, 1
	jal .return
	
	
.return:
	# This block contains cleanup code so it should always be called on function return
	# Restore return address so we return to original caller
	lw ra, 8(sp)
	
	# Restore stack pointer
	addi sp, sp, 12
	ret
	
	
	
.end: