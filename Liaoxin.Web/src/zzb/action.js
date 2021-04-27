export default function zzbAction(type, { record, form, query }) {
  if (type === 'CeShi') {
    form.setFieldsValue({ PlayerId: record.PlayerId });
    query();
  }
  if (type === 'UpToTop') {
    form.setFieldsValue({ PlayerId: null });
    query();
  }
}
