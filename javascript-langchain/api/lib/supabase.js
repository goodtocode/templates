import { createClient } from '@supabase/supabase-js'

    const supabaseUrl = "https://esbiazrlyamtdgfkrrbt.supabase.co"
    const supabaseAnon = "API-KEY"

    export const supabase = createClient(supabaseUrl, supabaseAnon)