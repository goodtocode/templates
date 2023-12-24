import { createClient } from '@supabase/supabase-js'

    const supabaseUrl = "https://esbiazrlyamtdgfkrrbt.supabase.co"
    const supabaseAnon = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImVzYmlhenJseWFtdGRnZmtycmJ0Iiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcwMzM1NTY0MSwiZXhwIjoyMDE4OTMxNjQxfQ.WfF4FrwQhG811SUi8UTrIgbdelRjwEB_pZeuNlvnd-4"

    export const supabase = createClient(supabaseUrl, supabaseAnon)