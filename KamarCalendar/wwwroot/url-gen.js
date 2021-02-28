const { createElement: e, useCallback, useState } = React;

function UrlGen() {
  const [isMMC, setIsMMC] = useState(true);
  const [portalAddress, setPortalAddress] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [icalAddress, setIcalAddress] = useState(null);

  const onChange = useCallback(({ target }) => {
    switch (target.name) {
      case 'school':
        setIsMMC(target.value === 'MMC');
        break;
      case 'portalAddress':
        setPortalAddress(target.value);
        break;
      case 'username':
        setUsername(target.value);
        break;
      case 'password':
        setPassword(target.value);
        break;
    }
  });

  const onSubmit = useCallback(
    async (ev) => {
      ev.preventDefault();

      try {
        setIcalAddress(
          await generateIcalAddress(
            isMMC ? '_' : portalAddress,
            username,
            password
          )
        );
      } catch (e) {
        setIcalAddress(e.toString());
      }
    },
    [isMMC, portalAddress, username, password]
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
    !isMMC &&
      e(
        'p',
        null,
        e(
          'label',
          null,
          'KAMAR Portal Address:',
          e('input', { name: 'portalAddress', value: portalAddress, onChange })
        )
      ),
    e(
      'p',
      null,
      e(
        'label',
        null,
        'Username:',
        e('input', { name: 'username', value: username, onChange })
      )
    ),
    e(
      'p',
      null,
      e(
        'label',
        null,
        'Password:',
        e('input', {
          name: 'password',
          value: password,
          onChange,
          type: 'password',
        })
      )
    ),
    e('button', { type: 'submit' }, 'Generate URL'),
    icalAddress &&
      e('p', null, e('a', { href: icalAddress, target: '_blank' }, icalAddress))
  );
}

async function generateIcalAddress(portalAddress, username, password) {
  return `${location.origin}/ICal/${portalAddress}/${username}/${password}`;
}

ReactDOM.render(e(UrlGen), document.getElementById('url-gen-root'));
