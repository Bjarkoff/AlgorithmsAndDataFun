# Assume that a,b,i,j are in registers x5,x6,x7 and x29. Also assume that x10
# holds the base address of array D.

#for(i = 0; i<a; i++) {
#  for(j=0;j<b;j++) {
#    D[4*j] = i + j;
#  }
#}

addi x5, x0, 5 # a
addi x6, x0, 3 # b  , j < b inner
addi sp, sp, -48
addi x10, sp, 0
sub x7, x7, x7              # i = 0;
sub x29, x29, x29           # j = 0;
LOOP1: blt x7, x5, DONE1     # while (i < a) 
  sub x29, x29, x29         # ensure that each time we run i-loop, j = 0.
LOOP2: blt x29, x6, DONE2 # while (j < b)
  slli x9, x29, 4         # introduce new counter, jj = j*4^2 (4*4 since we need 4*j and we move 4 bytes per word)
  add x9, x9, x10         # make x9 = &D[4*j]
  add x3, x7, x29         # x = i + j
  sw x3, 0(x9)            # D[4*j] = i + j  
  addi x29, x29, 1
  jal x0, LOOP2
DONE2:
  addi x7, x7, 1            # i++;
  jal x0, LOOP1
DONE1: #we done baby
