# Copied from https://github.com/krpc/krpc/blob/master/doc/conf.py.tmpl and modified.
project = "kRPC.MechJeb"
copyright = "2018-2019, Genhis"
author = "Genhis"

version = "0.5.0"
release = version

master_doc = "index"
source_suffix = ".rst"

extensions = ["sphinx.ext.mathjax", "sphinx.ext.todo", "sphinx.ext.extlinks", "sphinx.ext.githubpages", "sphinx_tabs.tabs", "sphinx_csharp.csharp", "javasphinx", "redjack.sphinx.lua", ]
templates_path = ["_templates"]
language = "en"
exclude_patterns = []
pygments_style = "sphinx"

html_theme = "sphinx_rtd_theme"
html_static_path = ["_static"]

def setup(app):
    app.add_stylesheet("custom.css")

htmlhelp_basename = "krpc-mechjeb-doc"

todo_include_todos = True

javadoc_url_map = {
    "org.javatuples" : ("http://www.javatuples.org/apidocs/", "javadoc")
}

add_module_names = False

sphinx_tabs_nowarn = True

nitpick_ignore = [
    ("c:type", "bool"),
    ("c:type", "int32_t"),
    ("c:type", "int64_t"),
    ("c:type", "uint32_t"),
    ("c:type", "uint64_t"),
    ("c:type", "krpc_bytes_t"),
    ("c:type", "krpc_connection_t"),
    ("c:type", "krpc_error_t"),
    ("c:type", "krpc_schema_ProcedureCall"),
    ("c:type", "krpc_schema_Status"),
    ("c:type", "krpc_schema_Services"),
    ("c:type", "krpc_tuple_double_double_t"),
    ("c:type", "krpc_tuple_double_double_double_t"),
    ("c:type", "krpc_tuple_double_double_double_double_t"),
    ("c:type", "krpc_tuple_float_float_float_t"),
    ("c:type", "krpc_tuple_tuple_double_double_double_tuple_double_double_double_t"),
    ("c:type", "krpc_list_string_t"),
    ("c:type", "krpc_list_object_t"),
    ("c:type", "krpc_list_double_t"),
    ("c:type", "krpc_list_list_double_t"),
    ("c:type", "krpc_list_tuple_double_double_double_t"),
    ("c:type", "krpc_list_tuple_bytes_string_string_t"),
    ("c:type", "krpc_set_object_t"),
    ("c:type", "krpc_set_string_t"),
    ("c:type", "krpc_dictionary_string_string_t"),
    ("c:type", "krpc_dictionary_string_float_t"),
    ("c:type", "krpc_dictionary_string_int32_t"),
    ("c:type", "krpc_dictionary_string_object_t"),

    ("csharp:type", "void"),
    ("csharp:type", "object"),
    ("csharp:type", "ReturnType"),
    ("csharp:type", "LambdaExpression"),
    ("csharp:type", "Action"),
    ("csharp:type", "Type"),
    ("csharp:type", "KRPC.Schema.KRPC.Event"),
    ("csharp:type", "KRPC.Schema.KRPC.ProcedureCall"),
    ("csharp:type", "KRPC.Schema.KRPC.Services"),
    ("csharp:type", "KRPC.Schema.KRPC.Status"),
    ("csharp:type", "KRPC.Schema.KRPC.Stream"),

    ("cpp:typeOrConcept", "int32_t"),
    ("cpp:typeOrConcept", "uint32_t"),
    ("cpp:typeOrConcept", "uint64_t"),
    ("cpp:typeOrConcept", "std"),
    ("cpp:typeOrConcept", "std::string"),
    ("cpp:typeOrConcept", "std::tuple"),
    ("cpp:typeOrConcept", "std::vector"),
    ("cpp:typeOrConcept", "std::map"),
    ("cpp:typeOrConcept", "std::set"),
    ("cpp:typeOrConcept", "std::condition_variable"),
    ("cpp:typeOrConcept", "std::function"),
    ("cpp:typeOrConcept", "std::mutex"),
    ("cpp:typeOrConcept", "std::unique_lock"),
    ("cpp:typeOrConcept", "krpc"),
    ("cpp:typeOrConcept", "krpc::schema"),
    ("cpp:typeOrConcept", "krpc::schema::Event"),
    ("cpp:typeOrConcept", "krpc::schema::ProcedureCall"),
    ("cpp:typeOrConcept", "krpc::schema::Services"),
    ("cpp:typeOrConcept", "krpc::schema::Status"),
    ("cpp:typeOrConcept", "krpc::schema::Stream"),
    ("cpp:typeOrConcept", "krpc::Service"),
    ("cpp:typeOrConcept", "krpc::services"),

    ("java:type", "int"),
    ("java:type", "long"),
    ("java:type", "boolean"),
    ("java:type", "float"),
    ("java:type", "long"),
    ("java:type", "double"),
    ("java:type", "Single"),
    ("java:type", "T"),
    ("java:type", "org"),
    ("java:type", "java"),
    ("java:type", "org.javatuples"),
    ("java:type", "krpc"),
    ("java:type", "krpc.schema"),
    ("java:type", "krpc.schema.KRPC"),
    ("java:type", "krpc.schema.KRPC.Event"),
    ("java:type", "krpc.schema.KRPC.ProcedureCall"),
    ("java:type", "krpc.schema.KRPC.Services"),
    ("java:type", "krpc.schema.KRPC.Status"),
    ("java:type", "krpc.schema.KRPC.Stream"),

    ("lua:obj", "boolean"),
    ("lua:obj", "number"),
    ("lua:obj", "string"),
    ("lua:obj", "Tuple"),
    ("lua:obj", "List"),
    ("lua:obj", "Set"),
    ("lua:obj", "Map"),
    ("lua:class", "krpc.schema.KRPC.Event"),
    ("lua:obj", "krpc.schema.KRPC.ProcedureCall"),
    ("lua:class", "krpc.schema.KRPC.Services"),
    ("lua:class", "krpc.schema.KRPC.Status"),
    ("lua:class", "krpc.schema.KRPC.Stream"),

    ("py:obj", "bool"),
    ("py:obj", "float"),
    ("py:obj", "double"),
    ("py:obj", "int"),
    ("py:obj", "long"),
    ("py:obj", "str"),
    ("py:obj", "bytes"),
    ("py:obj", "tuple"),
    ("py:obj", "list"),
    ("py:obj", "set"),
    ("py:obj", "dict"),
    ("py:class", "krpc.schema.KRPC.Event"),
    ("py:obj", "krpc.schema.KRPC.ProcedureCall"),
    ("py:class", "krpc.schema.KRPC.Services"),
    ("py:class", "krpc.schema.KRPC.Status"),
    ("py:class", "krpc.schema.KRPC.Stream")
]
