import { createClient } from '@supabase/supabase-js'

    const supabaseUrl = "URL"
    const supabaseAnon = "KEY"
    export const supabase = createClient(supabaseUrl, supabaseAnon)