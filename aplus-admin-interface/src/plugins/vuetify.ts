/**
 * plugins/vuetify.ts
 *
 * Framework documentation: https://vuetifyjs.com`
 */

// Styles
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'

// Composables
import { createVuetify } from 'vuetify'
import { pt } from 'vuetify/locale'

// https://vuetifyjs.com/en/introduction/why-vuetify/#feature-guides
 const vuetify = createVuetify({
  theme: {
    defaultTheme: 'light',
  },
  locale: {
    locale: 'pt',
    messages: { pt }
  }
})

export default vuetify;