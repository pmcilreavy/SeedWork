import { ErrorMessage, Field, Form, Formik } from 'formik';
import { useRootStore } from 'hooks/useRootStore';
import { observer } from 'mobx-react-lite';
import * as React from 'react';
import { useEffect } from 'react';
import * as Yup from 'yup';

export const ListTodos: React.FC = observer(() => {
  const { todos, loadTodos, createTodo } = useRootStore((store) => ({
    todos: store.todos.todos,
    loadTodos: store.todos.listTodos,
    createTodo: store.todos.createTodo,
  }));

  useEffect(() => {
    loadTodos();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <>
      <Formik
        initialValues={{ title: '', description: '' }}
        validationSchema={Yup.object({
          title: Yup.string().label('Title').required(),
          description: Yup.string().label('Description').required(),
        })}
        onSubmit={(values, { setSubmitting, resetForm }) =>
          createTodo(values.title, values.description)
            .then(() => resetForm())
            .finally(() => setSubmitting(false))
        }>
        {({ isSubmitting }) => (
          <Form>
            <div>
              <label htmlFor="title">Title</label>
              <Field type="text" name="title" />
              <ErrorMessage name="title" component="div" />
            </div>

            <div>
              <label htmlFor="description">Description</label>
              <Field type="text" name="description" />
              <ErrorMessage name="description" component="div" />
            </div>
            <button type="submit" disabled={isSubmitting}>
              Add
            </button>
          </Form>
        )}
      </Formik>
      <hr />
      <ul>
        {todos.map((o) => (
          <li key={o.id}>
            {o.title}: {o.description}
          </li>
        ))}
      </ul>
    </>
  );
});

export default ListTodos;
