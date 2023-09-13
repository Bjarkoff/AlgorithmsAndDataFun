addi    x6, x0, 10
addi    x5, x0, 0
LOOP: beq x6, x0, DONE
      addi x6, x6, -1
      addi x5, x5, 2
      jal x0, LOOP
DONE:

# If x6 = 10 og x5 = 0 to start, then after running we should have x5=20.