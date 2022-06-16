import { ErrorMessage, Field, Form, Formik } from 'formik';
import { useRootStore } from 'hooks/useRootStore';
import { observer } from 'mobx-react-lite';
import * as React from 'react';
import { useEffect } from 'react';
import * as Yup from 'yup';
import { StarIcon } from '@heroicons/react/outline';

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
      <div className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
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
              <div className="mb-4">
                <label htmlFor="title" className="block text-gray-700 text-sm font-bold mb-2">
                  Title
                </label>
                <Field
                  type="text"
                  name="title"
                  className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                />
                <ErrorMessage name="title" component="div" className="text-red-600" />
              </div>

              <div className="mb-6">
                <label htmlFor="description" className="block text-gray-700 text-sm font-bold mb-2">
                  Description
                </label>
                <Field
                  type="text"
                  name="description"
                  className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                />
                <ErrorMessage name="description" component="div" className="text-red-600" />
              </div>
              <div className="flex items-center justify-between mb-6">
                <button
                  type="submit"
                  disabled={isSubmitting}
                  className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">
                  Add
                </button>
              </div>
            </Form>
          )}
        </Formik>
      </div>
      <div className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
        <ul>
          {todos.map((o) => (
            <li key={o.id} className="mb-2 bg-slate-100 p-4 rounded-sm hover:bg-slate-200">
              <div className="flex justify-between items-center">
                <div>⚪</div>
                <div className="capitalize grow mx-4">{o.title}</div>
                <div>
                  <StarIcon className="h-5 w-5 text-yellow-500" />
                </div>
              </div>

              {/* <div className="capitalize">{o.description}</div> */}
            </li>
          ))}
        </ul>
      </div>
    </>
  );
});

export default ListTodos;
