const { createElement: e, useCallback, useState } = React;

function UrlGen() {
  const [isMMC, setIsMMC] = useState(true);

  const onChange = useCallback(({ target }) => {
    switch (target.name) {
      case 'school':
        setIsMMC(target.value === 'MMC');
        break;
    }
  });

  const onSubmit = useCallback(
    (ev) => {
      ev.preventDefault();
      console.log(isMMC);
    },
    [isMMC]
  );

  return e(
    'form',
    { onSubmit },
    e(
      'p',
      null,
      'School:',
      e(
        'label',
        null,
        e('input', {
          type: 'radio',
          name: 'school',
          value: 'MMC',
          checked: isMMC,
          onChange,
        }),
        'Mount Maunganui College'
      ),
      e(
        'label',
        null,
        e('input', {
          type: 'radio',
          name: 'school',
          value: 'other',
          checked: !isMMC,
          onChange,
        }),
        'Another School'
      )
    ),
    e('button', { type: 'submit' }, 'Generate URL')
  );
}

ReactDOM.render(e(UrlGen), document.getElementById('url-gen-root'));
