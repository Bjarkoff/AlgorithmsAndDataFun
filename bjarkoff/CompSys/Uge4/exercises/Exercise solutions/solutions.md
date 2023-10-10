# Exercises from OSTEP chapter 5, p. 14

## Question 1
* The values are copied to the child process (In fact own memory space is allocated for child process). When both parent and child change, they change their own instance of the variable, so it is not summed up so to say.


## Question 2
* Parent and child can write independently yeilding som interesting results!
* Note open() is syscall not part of stdlib. Returns FD as an int. FILE* returned by fopen() is an abstraction returning a buffer. Also works with fprinf, fscanf.
* As seen in the code race conditions may occur as it is up to the processor to schedule the write from both processes (Parent and child)


## Question 3
* Using wait or waitpid would be the obvious and easy approach to this problem.
* We will instead use signals to signla to the parent process, we will use SIG
* For students interested in signals and how they work --> https://www.geeksforgeeks.org/signals-c-language/

## Question 5
* Return values
    * On success, returns PID of terminated child. If more than 1 child, will be pid of arbitrary child.
    * On failure, returns -1, e.g. If any process has no child process.
* If child has no children (i.e. grandchildren), then wait will return -1 immediately

## Question 6
* Waitpid useful to wait for specific child instead of arbitrary child, like wait does.
*  waitpid(-1, &wstatus, 0); will have same effect as wait() as -1 means wait for an arbitrary child.
* Also possible to specify various options for waitpid, see linux man page
