import React from 'react';

export function Pagination({ value, onChangePaginationapi })  {
    const gotoPage = (link) => {
        onChangePaginationapi(link)
    }

    return (<div className="pagination">
        <button onClick={() => this.gotoPage(value.firstPageLink)} disabled={!value.hasPrevious}>
            {"<<"}
        </button>{" "}
        <button onClick={() => this.gotoPage(value.previousPageLink)}
            disabled={!value.hasPrevious}>
            {"<"}
        </button>{" "}
        {value.HasNext}
        <button onClick={() => gotoPage(value.nextPageLink)}
            disabled={!value.hasNext}>
            {">"}
        </button>{" "}
        <button onClick={() => this.gotoPage(value.lastPageLink)} disabled={!value.hasNext}>
            {">>"}
        </button>{" "}
        {/*<span>*/}
        {/*    Page{" "}*/}
        {/*    <strong>*/}
        {/*        {value.currentPage} of {pageOptions.length}*/}
        {/*    </strong>{" "}*/}
        {/*</span>*/}
        {/*<span>*/}
        {/*    | Go to page:{" "}*/}
        {/*    <input*/}
        {/*        type="number"*/}
        {/*        defaultValue={value.currentPage}*/}
        {/*        onChange={(e) => {*/}
        {/*            const page = e.target.value ? Number(e.target.value) - 1 : 0;*/}
        {/*            this.gotoPage(page);*/}
        {/*        }}*/}
        {/*        style={{ width: "100px" }}*/}
        {/*    />*/}
        {/*</span>{" "}*/}
        {/*<select*/}
        {/*    value={value.pageSize}*/}
        {/*    onChange={(e) => {*/}
        {/*        setPageSize(Number(e.target.value));*/}
        {/*    }}*/}
        {/*>*/}
        {/*    {[10, 20, 30, 40, 50].map((pageSize) => (*/}
        {/*        <option key={pageSize} value={pageSize}>*/}
        {/*            Show {pageSize}*/}
        {/*        </option>*/}
        {/*    ))}*/}
        {/*</select>*/}
    </div>);
}