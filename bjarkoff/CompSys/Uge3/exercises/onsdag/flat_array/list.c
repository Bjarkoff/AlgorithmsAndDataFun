#include <stdlib.h>
#include <assert.h>
#include "list.h"


struct list {
  void **data;
  int length;
};

struct list* list_create(void) {
  struct list *list = malloc(sizeof(struct list));
  if (list == NULL) {
    return NULL;
  }
  list->length = 0;
  list->data = NULL;
  return list;
}

void list_free(struct list* list) {
  if (list == NULL)
    return;

  if (list->data != NULL) {
    // Free each element in the list
    for (int i = 0; i < list->length; ++i) {
      free(list->data[i]);
    }
    // Free array itself
    free(list->data);
  }
  // Free struct itself
  free(list);
}

int list_insert_first(struct list* list, void* data) {
  if (list == NULL) {
    return 1;
  }
  int old_length = list->length;
  int new_length = old_length + 1;

  void **old_data = list->data;
  void **new_data = malloc(new_length * sizeof(void*));
  if (new_data == NULL) {
    return 1;
  }
  new_data[0] = data;
  if (old_data != NULL) {
    for (int i = 0; i < old_length; i++) {
    new_data[i+1] = old_data[i];
    }
  free(old_data);
  }
  
  list->length = new_length;
  list->data = new_data;
  
  return 0;
}

int list_remove_first(struct list* list, void** dest) {
  if (list == NULL || list->length <= 0) {
    return 1;
  }
  *dest = list->data[0];
  for (int i = 0; i<list->length - 1; ++i) {
    list->data[i] = list->data[i+1];
  }
  list->length--;
  return 0;
}

void list_map(struct list* list, map_fn f, void *aux) {
  if (list == NULL) {
    return;
  }
  for (int i = 0; i < list->length; ++i) {
    list->data[i] = f(list->data[i],aux);
  }
}
