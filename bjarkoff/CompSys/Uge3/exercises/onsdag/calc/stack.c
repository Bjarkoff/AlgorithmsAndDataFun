#include <stdlib.h>
#include <assert.h>
#include "stack.h"


struct stack_node {
  void *data;
  struct stack_node *next;
};

struct stack {
  struct stack_node *head;
};

struct stack* stack_new(void) {
  struct stack *stack = malloc(sizeof(struct stack));
  assert(stack != NULL);
  stack->head = NULL;
  return stack;
}

void stack_free(struct stack* stack) {
  struct stack_node *node = stack->head;
  while (node != NULL) {
    struct stack_node *next = node->next;
    free(node);
    node = next;
  }
  free(stack);
}

int stack_empty(struct stack* stack) {
  return (stack->head == NULL);
}

void* stack_top(struct stack* stack) {
  if (stack_empty(stack)) {
    return NULL;
  }
  return stack->head->data;
}

void* stack_pop(struct stack* stack) {
  if (stack_empty(stack)) {
    return NULL;
  }
  void* out_data = stack->head->data;
  struct stack_node *next = stack->head->next;
  free(stack->head);
  stack->head = next;
  return out_data;
}

int stack_push(struct stack* stack, void* data) {
  struct stack_node *new_head = malloc(sizeof(struct stack_node));
  assert(new_head != NULL);
  new_head->data = data;
  new_head->next = stack->head;

  stack->head = new_head;
  return 0;
}
